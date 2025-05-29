using Microsoft.AspNetCore.Mvc;
using Stripe.Checkout;
using Microsoft.Extensions.Options;
using ExFit.DataAcces.Repository.IRepository;
using ExFit.Utilities;
using System.Security.Claims;
using ExFit.Models;
using Stripe;
using ExFit.Utilities;

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
        var plan = _uw.MembershipPlan.Get(p => p.Id == planId);
        if (plan == null)
            return NotFound();

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (userId == null)
            return Unauthorized();

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
            SuccessUrl = Url.Action("Success", "Payment",
                       new { area = "Basic", sessionId = "{CHECKOUT_SESSION_ID}" },
                       Request.Scheme),
            CancelUrl = Url.Action("Cancel", "Payment",
                       new { area = "Basic" }, Request.Scheme),
            Metadata = new Dictionary<string, string>
        {
            { "planId", plan.Id.ToString() },
            { "userId", userId }
        }
        };

        var service = new SessionService();
        var session = service.Create(options);

        return Redirect(session.Url);
    }


    [HttpGet]
    public async Task<IActionResult> Success(string sessionId)
    {
        if (string.IsNullOrEmpty(sessionId))
            return BadRequest("No session ID");

        //retrieve the session from Stripe
        var service = new SessionService();
        var session = await service.GetAsync(sessionId);

        //verify payment succeeded
        if (session.PaymentStatus != "paid")
        {
            return View("Cancel");
        }

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



    public IActionResult Success()
    {
        return View();
    }

    public IActionResult Cancel()
    {
        return View();
    }
}
