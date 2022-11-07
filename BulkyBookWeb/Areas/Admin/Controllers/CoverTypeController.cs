
using BulkyBook.DataAccess.Repository.IRepository;
using BulkyBook.Models;
using BulkyBookWeb.DataAccess;
using Microsoft.AspNetCore.Mvc;

namespace BulkyBookWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CoverTypeController : Controller
    {
        private readonly IUnitOfWork _context;

        public CoverTypeController(IUnitOfWork context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var objCoverTypeList = _context.CoverType.GetAll();
            return View(objCoverTypeList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _context.CoverType.Add(obj);
                _context.Save();
                TempData["success"] = "CoverType created sucessfully!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Edit(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            //var categoryFromDb = _context.Categories.Find(id);
            var coverTypeFromDbFirst = _context.CoverType.GetFirstOrDefault(x => x.Id == id);
            //var categoryFromDbSingle = _context.Categories.SingleOrDefault(x => x.Id == id);
            if (coverTypeFromDbFirst == null)
                return NotFound();
            return View(coverTypeFromDbFirst);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(CoverType obj)
        {
            if (ModelState.IsValid)
            {
                _context.CoverType.Update(obj);
                _context.Save();
                TempData["success"] = "CoverType update successfully!";
                return RedirectToAction("Index");
            }
            return View(obj);
        }

        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
                return NotFound();
            //var categoryFromDb = _context.Categories.Find(id);
            var coverTypeFromDbFirst = _context.CoverType.GetFirstOrDefault(x => x.Id == id);
            //var categoryFromDbSingle = _context.Categories.SingleOrDefault(x => x.Id == id);
            if (coverTypeFromDbFirst == null)
                return NotFound();
            return View(coverTypeFromDbFirst);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeletePost(int? id)
        {
            var obj = _context.CoverType.GetFirstOrDefault(x => x.Id == id);
            if (obj == null)
                return NotFound();

            _context.CoverType.Remove(obj);
            _context.Save();
            TempData["success"] = "CoverType delete successfully!";
            return RedirectToAction("Index");
        }
    }
}
