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
        //Zobrazeni
        [HttpGet]
        public IActionResult Index()
        {
            IEnumerable<GradeDTO> grades = _gradeService.GetAll();
            return View(grades);
        }
        //Vytvoreni noveho zaznamu
        [HttpGet]
        public IActionResult Create()
        {
            FillDropdowns();
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(GradeDTO newGrade)
        {
            await _gradeService.CreateAsync(newGrade);
            return RedirectToAction("Index");
        }
        //Editace zaznamu
        [HttpGet]
        public async Task<IActionResult> EditAsync(int id, GradeDTO gradeDTO)
        {
            var gradeToEdit = await _gradeService.FindByIdAsync(id);
            if (gradeToEdit == null)
            {
                return View("NotFound");
            }
            FillDropdowns();
            return View(gradeToEdit);
        }
        [HttpPost]
        public async Task<IActionResult> EditAsync(GradeDTO gradeDTO, int id)
        {
            await _gradeService.UpdateAsync(gradeDTO, id);
            return RedirectToAction("Index");
        }
        //Pomocne metody
        private void FillDropdowns()
        {
            GradesDropdownViewModels gradesDropdownData = _gradeService.GetGradesDropdown();
            ViewBag.Students = new SelectList(gradesDropdownData.Students, "Id", "LastName");
            ViewBag.Subjects = new SelectList(gradesDropdownData.Subjects, "Id", "Name");
        }
    }
}
