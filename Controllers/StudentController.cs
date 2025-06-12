using ASP.NetCoreMVC_SchoolSystem.DTO;
using ASP.NetCoreMVC_SchoolSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NetCoreMVC_SchoolSystem.Controllers
{
    public class StudentController : Controller
    {
        StudentService _studentService;
        private readonly ILogger<HomeController> _logger;
        public StudentController(StudentService studentService, ILogger<HomeController> logger)
        {
            _studentService = studentService;
            _logger = logger;
        }
        [HttpGet]
        public IActionResult Index()
        {
            var allStudents = _studentService.GetAll();
            return View(allStudents);
        }
        [HttpGet]
        [Authorize(Roles = "Principal, Admin")]
        public IActionResult Create()
        {
            _logger.LogWarning("Volana metoda create");
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Principal, Admin")]
        public async Task<IActionResult> CreateAsync(StudentDTO newStudent)
        {
            await _studentService.CreateAsync(newStudent);
            return RedirectToAction("Index");
        }
        [HttpGet]
        [Authorize(Roles = "Principal, Admin")]
        public async Task<IActionResult> EditAsync(int id)
        {
            var studentToEdit = await _studentService.GetByIdAsync(id);
            return View(studentToEdit);
        }
        [HttpPost]
        [Authorize(Roles = "Principal, Admin")]
        public async Task<IActionResult> EditAsync(StudentDTO studentDTO, int id)
        {
            await _studentService.UpdateAsync(studentDTO, id);
            return RedirectToAction("Index");
        }
        [HttpPost]
        [Authorize(Roles = "Principal, Admin")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _studentService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Search(string q)
        {
            var foundStudents = _studentService.GetByName(q);
            return View("Index", foundStudents);
        }
    }
}
