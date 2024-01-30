using Book.DataAccess.Repository.IRepository;
using Book.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace BookWeb.Areas.Customer.Controllers
{
    [Authorize]
    [Area("Customer")]
    public class ShoppingCart : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public ShoppingCart(
            ILogger<HomeController> logger,
            IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }


        public async Task<IActionResult> Index()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            IEnumerable<ShoppingCartItem> shoppingCartItemList = await _unitOfWork.ShoppingCartItem.GetAllAsync(includeProperties: "Product");
            shoppingCartItemList = shoppingCartItemList.Where<ShoppingCartItem>(u => u.UserId==userId);
            return View(shoppingCartItemList);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            var cartItem = _unitOfWork.ShoppingCartItem.Get(u=>u.Id==id);

            if(cartItem == null)
            {
                return Json(new {success = false});
            }

            _unitOfWork.ShoppingCartItem.Remove(cartItem);
            await _unitOfWork.SaveAsync();
            return Json(new {success = true, message = "Delete Successfuly"});
        }

        [HttpPost]
        public async Task<IActionResult> ModifyAmount(int? id, int amount)
        {
           var cartItem = _unitOfWork.ShoppingCartItem.Get(u => u.Id == id);
            if(cartItem == null)
            {
                return Json(new {success = false, message = "Failed to get CartItem"});
            }

            cartItem.Amount = amount;
            _unitOfWork.ShoppingCartItem.Update(cartItem);
            await _unitOfWork.SaveAsync();
            return Json(new {success = true, message = "Cart amount modified"});
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
