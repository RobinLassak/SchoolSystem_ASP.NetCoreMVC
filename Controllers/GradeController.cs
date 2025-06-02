using ASP.NetCoreMVC_SchoolSystem.DTO;
using ASP.NetCoreMVC_SchoolSystem.Services;
using ASP.NetCoreMVC_SchoolSystem.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ASP.NetCoreMVC_SchoolSystem.Controllers
{
    public class GradeController : Controller
    {
        GradeService _gradeService;
        public GradeController(GradeService gradeService)
        {
            _gradeService = gradeService;
        }
        public IActionResult Index()
        {
            var grades = _gradeService.GetAll();
            return View(grades);
        }
        [HttpGet]
        public IActionResult Create()
        {
            GradesDropdownViewModels gradesDropdownData = _gradeService.GetGradesDropdown();
            ViewBag.Students = new SelectList(gradesDropdownData.Students, "Id", "LastName");
            ViewBag.Subjects = new SelectList(gradesDropdownData.Subjects, "Id", "Name");
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> CreateAsync(GradeDTO newGrade)
        {
            await _gradeService.CreateAsync(newGrade);
            return RedirectToAction("Index");
        }
    }
}
