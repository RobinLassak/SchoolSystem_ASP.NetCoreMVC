using ASP.NetCoreMVC_SchoolSystem.Models;

namespace ASP.NetCoreMVC_SchoolSystem.ViewModels
{
    public class GradesDropdownViewModels
    {
        public IEnumerable<Student> Students { get; set; }
        public IEnumerable<Subject> Subjects { get; set; }

        public GradesDropdownViewModels()
        {
            Students = new List<Student>();
            Subjects = new List<Subject>();
        }
    }
}
