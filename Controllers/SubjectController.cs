using ASP.NetCoreMVC_SchoolSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NetCoreMVC_SchoolSystem.Controllers
{
    public class SubjectController : Controller
    {
        SubjectService _subjectService;
        public SubjectController(SubjectService subjectService)
        {
            _subjectService = subjectService;
        }
        public IActionResult Index()
        {
            var allSubjects = _subjectService.GetAll();
            return View(allSubjects);
        }
    }
}
