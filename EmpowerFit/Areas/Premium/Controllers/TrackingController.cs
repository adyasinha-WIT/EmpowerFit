using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ExFit.DataAcces.Repository.IRepository;
using ExFit.Models;


namespace EmpowerFit.Areas.Premium.Controllers
{
    [Area("Premium")]
    public class TrackingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
