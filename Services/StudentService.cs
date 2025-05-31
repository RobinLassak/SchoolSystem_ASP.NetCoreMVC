using ASP.NetCoreMVC_SchoolSystem.Models;
using ASP.NetCoreMVC_SchoolSystem.DTO;

namespace ASP.NetCoreMVC_SchoolSystem.Services
{
    public class StudentService
    {
        private readonly SchoolDbContext _dbcontext;
        public StudentService(SchoolDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
        public List<StudentDTO> GetAll()
        {
            var AllStudents = _dbcontext.Students.ToList();
            var studentDtos = new List<StudentDTO>();
            foreach (var student in AllStudents)
            {
                StudentDTO studentDTO = new StudentDTO()
                {
                    Id = student.Id,
                    FirstName = student.FirstName,
                    LastName = student.LastName,
                    DateOfBirth = student.DateOfBirth,
                };
                studentDtos.Add(studentDTO);
            }
            return studentDtos;
        }

        internal async Task CreateAsync(StudentDTO newStudent)
        {
            Student studentToSave = new Student()
            {
                Id = newStudent.Id,
                FirstName = newStudent.FirstName,
                LastName = newStudent.LastName,
                DateOfBirth = newStudent.DateOfBirth,
            };
            _dbcontext.Students.Add(studentToSave);
            await _dbcontext.SaveChangesAsync();
        }
    }
}
