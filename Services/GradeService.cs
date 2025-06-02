
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
        //Zobrazeni
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
        //Vytvoreni noveho zaznamu
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

        internal GradesDropdownViewModels GetGradesDropdown()
        {
            return new GradesDropdownViewModels()
            {
                Students = _dbContext.Students.OrderBy(student => student.LastName),
                Subjects = _dbContext.Subjects.OrderBy(subject => subject.Name),
            };
            
        }
        //Pomocne metody
        private static GradeDTO ModelToDto(Grade grade)
        {
            return new GradeDTO()
            {
                Id = grade.Id,
                StudentName = grade.Student.LastName,
                SubjectName = grade.Subject.Name,
                StudentId = grade.Student.Id,
                SubjectId = grade.Subject.Id,
                Topic = grade.Topic,
                Mark = grade.Mark,
                Date = new DateOnly(grade.Date.Year, grade.Date.Month, grade.Date.Day),
            };
        }
    }
}
