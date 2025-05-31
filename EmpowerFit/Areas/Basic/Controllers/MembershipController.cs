using ExFit.DataAcces.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using ExFit.Models;
using System.Linq; 


namespace EmpowerFit.Areas.Basic.Controllers
{
    [Area("Basic")]
    [Authorize(Roles = "Basic,Premium")]
    public class MembershipController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<IdentityUser> _userManager;

        public MembershipController(IUnitOfWork unitOfWork, UserManager<IdentityUser> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var plans = _unitOfWork.MembershipPlan.GetAll().ToList();
            return View(plans);
        }

        [HttpPost]
        public IActionResult AddToCart(int planId)
        {
            var userId = _userManager.GetUserId(User);
            var existingCart = _unitOfWork.Cart.GetFirstOrDefault(c => c.UserId == userId);

            if (existingCart == null)
            {
                existingCart = new Cart { UserId = userId };
                _unitOfWork.Cart.Add(existingCart);
                _unitOfWork.Save();
            }

            var item = new CartItem { CartId = existingCart.Id, MembershipPlanId = planId };
            _unitOfWork.CartItem.Add(item);
            _unitOfWork.Save();

            return RedirectToAction("Cart");
        }

        public IActionResult Cart()
        {
            var userId = _userManager.GetUserId(User);
            var cart = _unitOfWork.Cart.GetFirstOrDefault(
                c => c.UserId == userId,
                includeProperties: "CartItems.MembershipPlan"
            );

            return View(cart);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult RemoveFromCart(int cartItemId)
        {
            var item = _unitOfWork.CartItem.Get(ci => ci.Id == cartItemId);
            if (item != null)
            {
                _unitOfWork.CartItem.Remove(item);
                _unitOfWork.Save();
            }
            return RedirectToAction(nameof(Cart));
        }



        [HttpPost]
        public IActionResult Checkout()
        {
            var userId = _userManager.GetUserId(User);
            var cart = _unitOfWork.Cart.GetFirstOrDefault(
                c => c.UserId == userId,
                includeProperties: "CartItems.MembershipPlan"
            );

            var user = _userManager.FindByIdAsync(userId).Result;

            if (cart != null && cart.CartItems.Any())
            {
                var plan = cart.CartItems.First().MembershipPlan;

                // Assign Premium role
                if (!_userManager.IsInRoleAsync(user, "Premium").Result)
                {
                    _userManager.AddToRoleAsync(user, "Premium").Wait();
                }


                _unitOfWork.Cart.Remove(cart); // Empty cart
                _unitOfWork.Save();
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
