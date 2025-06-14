using ASP.NetCoreMVC_SchoolSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NetCoreMVC_SchoolSystem.Controllers
{
    public class TeacherController : Controller
    {
        TeacherService _teacherService;
        public TeacherController(TeacherService teacherService)
        {
            _teacherService = teacherService;
        }
        //Zobrazeni vsech ucitelu
        public IActionResult Index()
        {
            var allTeachers = _teacherService.GetAll();
            return View(allTeachers);
        }
    }
}
