using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ExFit.DataAcces.Repository.IRepository;
using ExFit.Models;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;
using Exfit.Models;




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

        [HttpPost]
    public IActionResult SendLocation([FromBody] TemporaryLocationModel location)
    {
        if (location == null)
        return StatusCode(400, "Location data missing.");
            double lat = location.Latitude;
            double lon = location.Longitude;

            // TODO: create sms here

            return Ok("Location Recieved");
    }
}
}
