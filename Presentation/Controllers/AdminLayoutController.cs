using Microsoft.AspNetCore.Mvc;

namespace Presentation.Controllers
{
    public class AdminLayoutController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
