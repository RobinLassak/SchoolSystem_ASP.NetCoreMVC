
using ASP.NetCoreMVC_SchoolSystem.DTO;
using ASP.NetCoreMVC_SchoolSystem.Models;

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
        //Pomocne metody
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
