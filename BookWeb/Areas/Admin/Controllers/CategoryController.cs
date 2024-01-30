using Book.DataAccess.Repository;
using Book.DataAccess.Repository.IRepository;
using Book.Models;
using Book.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            IEnumerable<Category> objCategoryList = await _unitOfWork.Category.GetAllAsync();
            objCategoryList.ToList();
            return View(objCategoryList);
        }

        public IActionResult Upsert(int? id)
        {
            var category = new Category();
            if(id != null && id != 0)
            {
                category = _unitOfWork.Category.Get(u => u.Id == id);
                return View(category);
            }
            else
            {
                return View(category);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Upsert(Category obj)
        {
            if(ModelState.IsValid)
            {
                if(obj.Id == 0)
                {
                    _unitOfWork.Category.Add(obj);
                    TempData["success"] = "Category create successfully";
                } 
                else
                {
                    _unitOfWork.Category.Update(obj);
                    TempData["success"] = "Category edited successfully";
                }

                await _unitOfWork.SaveAsync();
                return RedirectToAction("Index");
            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Category? categoryFromDb = _unitOfWork.Category.Get(u => u.Id == id);

            if (categoryFromDb == null)
            {
                return NotFound();
            }
            return View(categoryFromDb);
        }

        [HttpPost, ActionName("Delete")]
        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _unitOfWork.Category.Get(c => c.Id == id);
            if (obj == null)
            {
                return NotFound();
            }

            _unitOfWork.Category.Remove(obj);
            _unitOfWork.SaveAsync();
            TempData["success"] = "Category deleted successfully";
            return RedirectToAction("Index");
        }
    }
}
