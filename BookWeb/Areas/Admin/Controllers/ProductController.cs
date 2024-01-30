using Book.DataAccess.Repository;
using Book.DataAccess.Repository.IRepository;
using Book.Models;
using Book.Models.ViewModels;
using Book.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Routing.Constraints;
using Microsoft.IdentityModel.Tokens;

namespace BookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnviroment;
        public ProductController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnviroment)
        {
            _unitOfWork = unitOfWork;
            _webHostEnviroment = webHostEnviroment;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Product> objProductList = await _unitOfWork.Product.GetAllAsync(includeProperties: "Category");
            objProductList = objProductList.ToList();
            return View(objProductList);

        }

        public IActionResult Upsert(int? id)
        {


            ProductVM productVM = new()
            {
                CategoryList = _unitOfWork.Category.GetAllAsync().Result.Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                }),
                Product = new Product()
            };

            if (id != null && id != 0)
            {
                //Update
                productVM.Product = _unitOfWork.Product.Get(u => u.Id == id);
            }

            return View(productVM);

        }

        [HttpPost]
        public IActionResult Upsert(ProductVM productVM, IFormFile? file)
        {
            if (ModelState.IsValid)
            {
                string wwwRootPath = _webHostEnviroment.WebRootPath;
                if (file != null)
                {
                    string fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    string productPath = Path.Combine(wwwRootPath, "images/product");

                    if (!string.IsNullOrEmpty(productVM.Product.ImageUrl))
                    {

                        //Delete old image
                        var oldImagePath =
                        Path.Combine(wwwRootPath, productVM.Product.ImageUrl.TrimStart('/'));

                        if (System.IO.File.Exists(oldImagePath))
                        {
                            System.IO.File.Delete(oldImagePath);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(productPath, fileName), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    productVM.Product.ImageUrl = @"/images/product/" + fileName;
                }



                if (productVM.Product.Id == 0)
                {
                    _unitOfWork.Product.Add(productVM.Product);
                }
                else
                {
                    _unitOfWork.Product.Update(productVM.Product);
                }

                _unitOfWork.SaveAsync();
                TempData["success"] = "Product created successfully";
                return RedirectToAction("Index");
            }
            else
            {
                productVM.CategoryList = _unitOfWork.Category.GetAllAsync().Result.Select(u => new SelectListItem
                {
                    Text = u.Name,
                    Value = u.Id.ToString(),
                });
                return View();
            }
        }

        #region APICALLS

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            IEnumerable<Product> objProductList = await _unitOfWork.Product.GetAllAsync(includeProperties: "Category");
            objProductList = objProductList.ToList();

            return Json(new { data = objProductList });
        }

        [HttpDelete]
        public IActionResult Delete(int? id)
        {
            var productToBeDeleted = _unitOfWork.Product.Get(u => u.Id == id);
            if (productToBeDeleted == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }

            if (!string.IsNullOrEmpty(productToBeDeleted.ImageUrl))
            {
                //Delete old image
                var oldImagePath =
                Path.Combine(_webHostEnviroment.WebRootPath,
                productToBeDeleted.ImageUrl.TrimStart('/'));

                Console.WriteLine(oldImagePath);
                if (System.IO.File.Exists(oldImagePath))
                {
                    System.IO.File.Delete(oldImagePath);
                }
            }

            _unitOfWork.Product.Remove(productToBeDeleted);
            _unitOfWork.SaveAsync();

            return Json(new { success = true, message = "Delete Successful" });
        }

        #endregion
    }
}
