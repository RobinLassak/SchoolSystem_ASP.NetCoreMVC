using ASP.NetCoreMVC_SchoolSystem.DTO;
using ASP.NetCoreMVC_SchoolSystem.Models;

namespace ASP.NetCoreMVC_SchoolSystem.Services
{
    public class SubjectService
    {
        private readonly SchoolDbContext _dbcontext;
        public SubjectService(SchoolDbContext dbContext)
        {
            _dbcontext = dbContext;
        }
        public List<SubjectDTO> GetAll()
        {
            var AllSubjects = _dbcontext.Subjects.ToList();
            var subjectsDtos = new List<SubjectDTO>();
            foreach (var subject in AllSubjects)
            {
                SubjectDTO subjectDTO = ModelToDto(subject);
                subjectsDtos.Add(subjectDTO);
            }
            return subjectsDtos;
        }
        private static SubjectDTO ModelToDto(Subject subject)
        {
            return new SubjectDTO()
            {
                Id = subject.Id,
                Name = subject.Name,
            };
        }
    }
}
