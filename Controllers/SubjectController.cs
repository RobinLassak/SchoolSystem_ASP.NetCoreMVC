using ASP.NetCoreMVC_SchoolSystem.DTO;
using ASP.NetCoreMVC_SchoolSystem.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ASP.NetCoreMVC_SchoolSystem.Controllers
{
    public class SubjectController : Controller
    {
        SubjectService _subjectService;
        private readonly ILogger<HomeController> _logger;
        public SubjectController(SubjectService subjectService, ILogger<HomeController> logger)
        {
            _subjectService = subjectService;
            _logger = logger;
        }
        //Metoda pro zobrazeni seznamu predmetu
        [HttpGet]
        public IActionResult Index()
        {
            var allSubjects = _subjectService.GetAll();
            return View(allSubjects);
        }
        //Metoda pro zobrazeni stranky Create
        [HttpGet]
        [Authorize(Roles = "Principal, Admin")]
        public IActionResult Create()
        {
            return View();
        }

        //Metoda pro ulozeni noveho predmetu do seznamu
        [HttpPost]
        [Authorize(Roles = "Principal, Admin")]
        public async Task<IActionResult> CreateAsync(SubjectDTO newSubject)
        {
            if (!ModelState.IsValid)
            {
                _logger.LogError("Neplatna data.");
                return View(newSubject);
            }
            await _subjectService.CreateAsync(newSubject);
            return RedirectToAction("Index");
        }
        //Metody pro editaci predmetu
        [HttpGet]
        [Authorize(Roles = "Principal, Admin")]
        public async Task<IActionResult> EditAsync(int id)
        {
            var subjectToEdit = await _subjectService.GetByIdAsync(id);
            if(subjectToEdit == null)
            {
                return View("NotFound");
            }
            return View(subjectToEdit);
        }
        [HttpPost]
        [Authorize(Roles = "Principal, Admin")]
        public async Task<IActionResult> EditAsync(SubjectDTO subjectDTO, int id)
        {
            await _subjectService.UpdateAsync(subjectDTO, id);
            return RedirectToAction("Index");
        }
        //Metoda pro smazani predmetu
        [HttpPost]
        [Authorize(Roles = "Principal, Admin")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            await _subjectService.DeleteAsync(id);
            return RedirectToAction("Index");
        }
    }
}
