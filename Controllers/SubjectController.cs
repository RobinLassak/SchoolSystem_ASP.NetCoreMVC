using ASP.NetCoreMVC_SchoolSystem.DTO;
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
        //Metoda pro zobrazeni seznamu predmetu
        [HttpGet]
        public IActionResult Index()
        {
            var allSubjects = _subjectService.GetAll();
            return View(allSubjects);
        }
        //Metoda pro zobrazeni stranky Create
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        //Metoda pro ulozeni noveho predmetu do seznamu
        [HttpPost]
        public async Task<IActionResult> CreateAsync(SubjectDTO newSubject)
        {
            await _subjectService.CreateAsync(newSubject);
            return RedirectToAction("Index");
        }

    }
}
