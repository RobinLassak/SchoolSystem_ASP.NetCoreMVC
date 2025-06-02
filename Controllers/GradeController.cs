using ASP.NetCoreMVC_SchoolSystem.Services;
using Microsoft.AspNetCore.Mvc;

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
            return View();
        }
    }
}
