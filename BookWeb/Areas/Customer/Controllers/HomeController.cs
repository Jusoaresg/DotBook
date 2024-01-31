using Book.DataAccess.Repository.IRepository;
using Book.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Security.Claims;

namespace BookWeb.Areas.Customer.Controllers
{
    [Area("Customer")]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(ILogger<HomeController> logger, IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            try
            {
                IEnumerable<Product> productList = await _unitOfWork.Product.GetAllAsync(includeProperties: "Category");
                return View(productList);
            }
            catch(Exception ex)
            {
                return RedirectToAction("Error");
            }
        }

        public IActionResult Details(int productId)
        {
            Product product = _unitOfWork.Product.Get(u=>u.Id==productId, includeProperties: "Category");
            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> Details(Product product, int amount)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(userId))
            {

                IEnumerable<ShoppingCartItem> userItems = await _unitOfWork.ShoppingCartItem.GetAllAsync(includeProperties: "Product");
                userItems = userItems.Where(u => u.UserId == userId);
                foreach(var obj in userItems)
                {
                    if(obj.ProductId == product.Id)
                    {
                        obj.Amount += amount;
                        _unitOfWork.ShoppingCartItem.Update(obj);
                        await _unitOfWork.SaveAsync();
                        return RedirectToAction("Index", "ShoppingCart");
                    }
                }

                double price = 0;
                if(amount > 100)
                { 
                    price = product.Price100; 
                }
                else if (amount > 50) 
                {
                    price = product.Price50;
                }
                else 
                {
                    price = product.Price;
                }

                ShoppingCartItem item = new ShoppingCartItem{UserId = userId, ProductId = product.Id, Amount = amount, Price = price};
                _unitOfWork.ShoppingCartItem.Add(item);
                await _unitOfWork.SaveAsync();
                return RedirectToAction("Index", "ShoppingCart");
            }
            return RedirectToAction("Login", "Account", new {area = "Identity"});
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
