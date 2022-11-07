
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBookWeb.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly IUnitOfWork _context;

        public CategoryController(IUnitOfWork context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var objCategoryList = _context.Category.GetAll();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "The display order cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _context.Category.Add(obj);
                _context.Save();
                TempData["success"] = "Category created sucessfully!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            //var categoryFromDb = _context.Categories.Find(id);
            var categoryFromDbFirst = _context.Category.GetFirstOrDefault(x => x.Id == id);
            //var categoryFromDbSingle = _context.Categories.SingleOrDefault(x => x.Id == id);
            if (categoryFromDbFirst == null)
                return NotFound();
            return View(categoryFromDbFirst);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Category obj)
        {
            if (obj.Name == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("CustomError", "The display order cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
                _context.Category.Update(obj);
                _context.Save();
                TempData["success"] = "Category update successfully!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            //var categoryFromDb = _context.Categories.Find(id);
            var categoryFromDbFirst = _context.Category.GetFirstOrDefault(x => x.Id == id);
            //var categoryFromDbSingle = _context.Categories.SingleOrDefault(x => x.Id == id);
            if (categoryFromDbFirst == null)
                return NotFound();
            return View(categoryFromDbFirst);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _context.Category.GetFirstOrDefault(x => x.Id == id);
            if (obj == null)
                return NotFound();

            _context.Category.Remove(obj);
            _context.Save();
            TempData["success"] = "Category delete successfully!";
            return RedirectToAction("Index");
        }
    }
}
