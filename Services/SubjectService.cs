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
        //Zobrazeni seznamu predmetu
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
        //Vytvoreni noveho predmetu
        internal async Task CreateAsync(SubjectDTO newSubject)
        {
            Subject subjectToSave = DtoToModel(newSubject);
            _dbcontext.Subjects.Add(subjectToSave);
            await _dbcontext.SaveChangesAsync();
        }
        //Uprava stavajiciho predmetu
        internal async Task<SubjectDTO> GetByIdAsync(int id)
        {
            var subjectToEdit = await _dbcontext.Subjects.FindAsync(id);
            return ModelToDto(subjectToEdit);
        }
        internal async Task UpdateAsync(SubjectDTO subjectDTO, int id)
        {
            _dbcontext.Update(DtoToModel(subjectDTO));
            await _dbcontext.SaveChangesAsync();
        }
        //Smazani predmetu
        internal async Task DeleteAsync(int id)
        {
            var subjectToDelete = await _dbcontext.Subjects.FindAsync(id);
            if (subjectToDelete != null)
            {
                _dbcontext.Subjects.Remove(subjectToDelete);
            }
            await _dbcontext.SaveChangesAsync();
        }
        //Pomocne metody
        private static SubjectDTO ModelToDto(Subject subject)
        {
            return new SubjectDTO()
            {
                Id = subject.Id,
                Name = subject.Name,
            };
        }
        private Subject DtoToModel(SubjectDTO newSubject)
        {
            return new Subject()
            {
                Id = newSubject.Id,
                Name = newSubject.Name,  
            };
        }
    }
}
