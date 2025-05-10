using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ExFit.DataAcces.Repository.IRepository;
using ExFit.Models;
using Microsoft.AspNetCore.Authorization;


namespace EmpowerFit.Areas.Premium.Controllers
{
    [Area("Premium")]
    [Authorize(Roles = "Premium")]
    public class TrackingController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
