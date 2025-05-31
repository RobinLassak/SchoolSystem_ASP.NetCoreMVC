using ASP.NetCoreMVC_SchoolSystem.Services;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NetCoreMVC_SchoolSystem.Controllers
{
    public class StudentController : Controller
    {
        StudentService _studentService;
        public StudentController(StudentService studentService)
        {
            _studentService = studentService;
        }
        public IActionResult Index()
        {
            var allStudents = _studentService.GetAll();
            return View(allStudents);
        }
    }
}
