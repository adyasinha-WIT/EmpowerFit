using Microsoft.AspNetCore.Mvc;

namespace EmpowerFit.Areas.Basic.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
