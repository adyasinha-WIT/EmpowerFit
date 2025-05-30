using Microsoft.AspNetCore.Mvc;
using Stripe;
using Stripe.Checkout;
using Microsoft.Extensions.Options;
using ExFit.DataAcces.Repository.IRepository;
using ExFit.Utilities;
using System.Security.Claims;
using ExFit.Models;

[Area("Basic")]
public class PaymentController : Controller
{
    private readonly StripeSettings _stripe;
    private readonly IUnitOfWork _uw;

    public PaymentController(IOptions<StripeSettings> stripeOpts,
                             IUnitOfWork uw)
    {
        _stripe = stripeOpts.Value;
        _uw = uw;
    }

    [HttpPost]
    public IActionResult CreateCheckoutSession(int planId)
    {
        //to catch the issue: both keys were swapped in JSON
        Console.WriteLine($"⇢ stripe.PK = {_stripe.PublishableKey}");
        Console.WriteLine($"⇢ stripe.SK = {_stripe.SecretKey}");
        // 1) load plan
        var plan = _uw.MembershipPlan.Get(p => p.Id == planId);
        if (plan == null) return NotFound();

        // 2) get current user
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null) return Unauthorized();

        var successPath = Url.Action("Success", "Payment", new { area = "Basic" });
        var cancelPath = Url.Action("Cancel", "Payment", new { area = "Basic" });

        // 3) build checkout options
        var options = new SessionCreateOptions
        {
            PaymentMethodTypes = new List<string> { "card" },
            LineItems = new List<SessionLineItemOptions>
            {
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        UnitAmount  = (long)(plan.Price * 100),
                        Currency    = "usd",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = $"{plan.DurationInDays}-Day Membership"
                        }
                    },
                    Quantity = 1
                }
            },
            Mode = "payment",
            SuccessUrl = $"{Request.Scheme}://{Request.Host}{successPath}?sessionId={{CHECKOUT_SESSION_ID}}",
            CancelUrl = $"{Request.Scheme}://{Request.Host}{cancelPath}",
            Metadata = new Dictionary<string, string>
            {
                { "planId", plan.Id.ToString() },
                { "userId", userId }
            }
        };

        //instantiating a Stripe client with your secret key
        var client = new StripeClient(_stripe.SecretKey);
        var service = new SessionService(client);

        //create the session
        var session = service.Create(options);

        return Redirect(session.Url);
    }

    [HttpGet]
    public async Task<IActionResult> Success(string sessionId)
    {
        if (string.IsNullOrEmpty(sessionId))
            return BadRequest("No session ID");

        var client = new StripeClient(_stripe.SecretKey);
        var service = new SessionService(client);
        var session = await service.GetAsync(sessionId);

        //verify payment
        if (session.PaymentStatus != "paid")
            return View("Cancel");

        var planId = int.Parse(session.Metadata["planId"]);
        var userId = session.Metadata["userId"];
        var plan = _uw.MembershipPlan.Get(p => p.Id == planId);
        if (plan == null) return NotFound();

        var membership = new UserMembership
        {
            UserId = userId,
            PlanId = planId,
            PurchaseDate = DateTime.UtcNow,
            ExpiryDate = DateTime.UtcNow.AddDays(plan.DurationInDays)
        };
        _uw.UserMembership.Add(membership);
        _uw.Save();

        return View();
    }

    public IActionResult Cancel()
    {
        return View();
    }
}
