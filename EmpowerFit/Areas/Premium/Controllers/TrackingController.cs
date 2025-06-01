using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ExFit.DataAcces.Repository.IRepository;
using ExFit.Models;
using Microsoft.AspNetCore.Authorization;
using Newtonsoft.Json.Linq;
using Exfit.Models;
using ExFit.Utilities;
using System;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Identity;




namespace EmpowerFit.Areas.Premium.Controllers
{
    [Area("Premium")]
    [Authorize(Roles = "Premium")]
    public class TrackingController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly TwilioSettings _twilioSettings;

        public TrackingController(IOptions<TwilioSettings> twilioOpts, UserManager<IdentityUser> userManager)
        {
            _twilioSettings = twilioOpts.Value;
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
    public async Task<IActionResult> SendLocation([FromBody] TemporaryLocationModel location)
    {
        if (location == null)
        return StatusCode(400, "Location data missing.");
            double lat = location.Latitude;
            double lon = location.Longitude;
            string userName = User.Identity.Name;
      
            // TODO: create sms here
            try
            {
                TwilioClient.Init(_twilioSettings.AccountSid, _twilioSettings.AuthToken);

                var message = await MessageResource.CreateAsync(
                    body: "SOS Alert from user:"+ userName+". User location is:\nLatitude:" + lat + "\nLongitude:" + lon,
                    from: new Twilio.Types.PhoneNumber(_twilioSettings.FromPhone),
                    to: new Twilio.Types.PhoneNumber("+64277664885")
                    );

                Console.WriteLine(message.Body);


                return Ok("Location Recieved and SMS sent");
            }
            catch
            {
                return StatusCode(500, "Error sending SMS.");
            }


        }
        
        
}
}
