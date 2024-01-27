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

        public IActionResult Index()
        {
            try
            {
                IEnumerable<Product> productList = _unitOfWork.Product.GetAll(includeProperties: "Category");
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
        public IActionResult Details(Product product, int amount)
        {
            string? userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (!string.IsNullOrEmpty(userId))
            {

                IEnumerable<ShoppingCartItem> userItems = _unitOfWork.ShoppingCartItem.GetAll(includeProperties: "Product").Where(u => u.UserId == userId);
                foreach(var obj in userItems)
                {
                    if(obj.ProductId == product.Id)
                    {
                        obj.Amount += amount;
                        _unitOfWork.ShoppingCartItem.Update(obj);
                        _unitOfWork.Save();
                        return RedirectToAction("Index", "ShoppingCart");
                    }
                }

                ShoppingCartItem item = new ShoppingCartItem{UserId = userId, ProductId = product.Id, Amount = amount};
                _unitOfWork.ShoppingCartItem.Add(item);
                _unitOfWork.Save();
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
