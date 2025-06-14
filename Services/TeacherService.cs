
using ASP.NetCoreMVC_SchoolSystem.DTO;
using ASP.NetCoreMVC_SchoolSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace ASP.NetCoreMVC_SchoolSystem.Services
{
    public class TeacherService
    {
        private readonly SchoolDbContext _dbContext;
        public TeacherService(SchoolDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        //Zobrazeni vsech ucitelu
        public List<TeacherDTO> GetAll()
        {
            var allTeachers = _dbContext.Teachers.ToList();
            var teacherDtos = new List<TeacherDTO>();
            foreach (var teacher in allTeachers)
            {
                TeacherDTO teacherDTO = ModelToDto(teacher);
                teacherDtos.Add(teacherDTO);
            }
            return teacherDtos;
        }
        //Vytvoreni noveho ucitele
        internal async Task CreateAsync(TeacherDTO newTeacher)
        {
            Teacher teacherToSave = DtoToModel(newTeacher);
            _dbContext.Teachers.Add(teacherToSave);
            await _dbContext.SaveChangesAsync();
        }
        //Editace ucitele
        internal async Task<TeacherDTO> GetByIdAsync(int id)
        {
            var teacherToEdit = await _dbContext.Teachers.FindAsync(id);
            return ModelToDto(teacherToEdit);
        }
        internal async Task UpdateAsync(TeacherDTO teacherDTO, int id)
        {
            _dbContext.Update(DtoToModel(teacherDTO));
            await _dbContext.SaveChangesAsync();
        }
        //Pomocne metody
        private Teacher DtoToModel(TeacherDTO newTeacher)
        {
            return new Teacher()
            {
                Id = newTeacher.Id,
                FirstName = newTeacher.FirstName,
                LastName = newTeacher.LastName,
                DateOfBirth = newTeacher.DateOfBirth,
                Salary = newTeacher.Salary,
            };
        }
        private static TeacherDTO ModelToDto(Teacher teacher)
        {
            return new TeacherDTO()
            {
                Id = teacher.Id,
                FirstName = teacher.FirstName,
                LastName = teacher.LastName,
                DateOfBirth = teacher.DateOfBirth,
                Salary = teacher.Salary,
            };
        }
    }
}
