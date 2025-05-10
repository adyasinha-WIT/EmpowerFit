using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace EmpowerFit.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")] // Restrict access to Admins only
    public class ReportsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
