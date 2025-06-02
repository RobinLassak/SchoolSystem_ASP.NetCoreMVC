using ASP.NetCoreMVC_SchoolSystem.DTO;
using ASP.NetCoreMVC_SchoolSystem.Models;
using ASP.NetCoreMVC_SchoolSystem.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace ASP.NetCoreMVC_SchoolSystem.Services
{
    public class GradeService
    {
        private readonly SchoolDbContext _dbContext;

        public GradeService(SchoolDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        // Zobrazeni zaznamu
        public List<GradeDTO> GetAll()
        {
            var allGrades = _dbContext.Grades
                .Include(g => g.Student)
                .Include(g => g.Subject)
                .ToList();

            var gradesDtos = new List<GradeDTO>();
            foreach (var grade in allGrades)
            {
                GradeDTO gradeDTO = ModelToDto(grade);
                gradesDtos.Add(gradeDTO);
            }
            return gradesDtos;
        }

        // Vytvoreni noveho zaznamu
        internal async Task CreateAsync(GradeDTO newGrade)
        {
            Grade gradeToInsert = new Grade()
            {
                Mark = newGrade.Mark,
                Topic = newGrade.Topic,
                Student = await _dbContext.Students.FindAsync(newGrade.StudentId),
                Subject = await _dbContext.Subjects.FindAsync(newGrade.SubjectId),
                Date = DateTime.Now,
            };
            await _dbContext.Grades.AddAsync(gradeToInsert);
            await _dbContext.SaveChangesAsync();
        }

        // Dropdown data pro formulář
        internal GradesDropdownViewModels GetGradesDropdown()
        {
            return new GradesDropdownViewModels()
            {
                Students = _dbContext.Students.OrderBy(s => s.LastName),
                Subjects = _dbContext.Subjects.OrderBy(s => s.Name),
            };
        }

        // Najdi známku podle ID
        internal async Task<GradeDTO?> FindByIdAsync(int id)
        {
            var gradeToReturn = await _dbContext.Grades
                .Include(g => g.Student)
                .Include(g => g.Subject)
                .FirstOrDefaultAsync(g => g.Id == id);

            if (gradeToReturn == null)
                return null;

            return ModelToDto(gradeToReturn);
        }

        // Aktualizace záznamu
        internal async Task UpdateAsync(GradeDTO gradeDTO, int id)
        {
            var gradeToUpdate = await _dbContext.Grades.FindAsync(id);
            if (gradeToUpdate == null)
                return;

            gradeToUpdate.Topic = gradeDTO.Topic;
            gradeToUpdate.Mark = gradeDTO.Mark;
            gradeToUpdate.Date = gradeDTO.Date.ToDateTime(new TimeOnly(0));
            gradeToUpdate.Student = await _dbContext.Students.FindAsync(gradeDTO.StudentId);
            gradeToUpdate.Subject = await _dbContext.Subjects.FindAsync(gradeDTO.SubjectId);

            _dbContext.Update(gradeToUpdate);
            await _dbContext.SaveChangesAsync();
        }
        //Mazani zaznamu
        internal async Task DeleteAsync(int id)
        {
            var gradesToDelete = await _dbContext.Grades.FindAsync(id);
            if (gradesToDelete != null)
            {
                _dbContext.Grades.Remove(gradesToDelete);
            }
            await _dbContext.SaveChangesAsync();
        }

        // DTO konverze
        private static GradeDTO ModelToDto(Grade grade)
        {
            return new GradeDTO()
            {
                Id = grade.Id,
                StudentId = grade.Student.Id,
                StudentName = grade.Student.LastName,
                SubjectId = grade.Subject.Id,
                SubjectName = grade.Subject.Name,
                Topic = grade.Topic,
                Mark = grade.Mark,
                Date = new DateOnly(grade.Date.Year, grade.Date.Month, grade.Date.Day),
            };
        }
    }
}
