using Microsoft.AspNetCore.Mvc;

namespace AlMohamyProject.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ComingSoonController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ComingSoon()
        {
            return View();
        }
    }
}
