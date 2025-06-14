using ASP.NetCoreMVC_SchoolSystem.DTO;
using ASP.NetCoreMVC_SchoolSystem.Services;
using Microsoft.AspNetCore.Authorization;
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
        //Vytvoreni noveho ucitele
        [HttpGet]
        [Authorize(Roles = "Principal, Admin")]
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Principal, Admin")]
        public async Task<IActionResult> CreateAsync(TeacherDTO newTeacher)
        {
            await _teacherService.CreateAsync(newTeacher);
            return RedirectToAction("Index");
        }
    }
}
