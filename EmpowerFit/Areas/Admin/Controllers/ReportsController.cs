using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;

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

        public async Task<IActionResult> Index()
        {
            //Getting  user count
            var userCount = _userManager.Users.Count();

            //Getting counts for each roles 
            var basicCount= (await _userManager.GetUsersInRoleAsync("Basic")).Count;
            var premiumCount = (await _userManager.GetUsersInRoleAsync("Premium")).Count;
            var adminCount = (await _userManager.GetUsersInRoleAsync("Admin")).Count;

            //Passing the data to the view
            ViewBag.BasicCount = basicCount;
            ViewBag.PremiumCount = premiumCount;
            ViewBag.AdminCount = adminCount;
            ViewBag.TotalCount = userCount;

            return View();
        }
    }
}
