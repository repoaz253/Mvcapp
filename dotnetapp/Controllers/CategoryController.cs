using dotnetapp.Data;
using dotnetapp.Models;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace dotnetapp.Controllers
{
    public class CategoryController : Controller

    {

        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;

        }
        public IActionResult Index()
        {

            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Category obj)
        {
            if (obj.Name.ToLower() == obj.DisplayOrder.ToString())
            {
                ModelState.AddModelError("Name", "The display order and display name cannot be same");
            }
            if (ModelState.IsValid)
            {
                _db.Categories.Add(obj);
                _db.SaveChanges();
                TempData["success"] = "Category created sucessfully";
                return RedirectToAction("Index"); // if diff controller ("index", "controllername")

            }
            return View();
        }

        public IActionResult Edit(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            Category categoryfromDb = _db.Categories.First(u => u.Id == id);
            if (categoryfromDb == null)
            {
                return NotFound();
            }

            return View(categoryfromDb);
        }

        [HttpPost]
        public IActionResult Edit(Category obj)
        {

            if (ModelState.IsValid)
            {
                _db.Categories.Update(obj);
                _db.SaveChanges();
                TempData["success"] = "Category edited sucessfully";
                return RedirectToAction("Index"); // if diff controller ("index", "controllername")

            }
            return View();
        }

        public IActionResult Delete(int? id)
        {
            if (id == 0 || id == null)
            {
                return NotFound();
            }
            Category categoryfromDb = _db.Categories.First(u => u.Id == id);
            if (categoryfromDb == null)
            {
                return NotFound();
            }

            return View(categoryfromDb);
        }

        [HttpPost, ActionName("Delete")]

        public IActionResult DeletePOST(int? id)
        {
            Category? obj = _db.Categories.Find(id);
            {
                if (obj == null)
                {
                    return NotFound();
                }
                _db.Categories.Remove(obj);
                _db.SaveChanges();
                TempData["success"] = "Category deleted sucessfully";
                return RedirectToAction("Index"); // if diff controller ("index", "controllername")


            }
        }
    }
}
