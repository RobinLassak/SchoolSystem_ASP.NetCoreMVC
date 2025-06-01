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
                StudentDTO studentDTO = ModelToDto(student);
                studentDtos.Add(studentDTO);
            }
            return studentDtos;
        }


        internal async Task CreateAsync(StudentDTO newStudent)
        {
            Student studentToSave = DtoToModel(newStudent);
            _dbcontext.Students.Add(studentToSave);
            await _dbcontext.SaveChangesAsync();
        }
        internal async Task<StudentDTO> GetByIdAsync(int id)
        {
            var studentToEdit = await _dbcontext.Students.FindAsync(id);
            return ModelToDto(studentToEdit);
        }
        internal async Task UpdateAsync(StudentDTO studentDTO, int id)
        {
            _dbcontext.Update(DtoToModel(studentDTO));
            await _dbcontext.SaveChangesAsync();
        }
        private Student DtoToModel(StudentDTO newStudent)
        {
            return new Student()
            {
                Id = newStudent.Id,
                FirstName = newStudent.FirstName,
                LastName = newStudent.LastName,
                DateOfBirth = newStudent.DateOfBirth,
            };
        }
        private static StudentDTO ModelToDto(Student student)
        {
            return new StudentDTO()
            {
                Id = student.Id,
                FirstName = student.FirstName,
                LastName = student.LastName,
                DateOfBirth = student.DateOfBirth,
            };
        }
    }
}
