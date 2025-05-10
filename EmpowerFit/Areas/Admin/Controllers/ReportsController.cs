using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;

namespace EmpowerFit.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")] // Restrict access to Admins only
    public class ReportsController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;

        public ReportsController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            // Get user count
            var userCount = _userManager.Users.Count();


            ViewBag.UserCount = userCount;
            return View();
        }
    }
}
