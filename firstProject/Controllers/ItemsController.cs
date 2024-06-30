using firstProject.Data;
using firstProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.Options;
using Microsoft.EntityFrameworkCore;

namespace firstProject.Controllers
{
    public class ItemsController: Controller
	{
        private readonly AppDbContext _db;
        public ItemsController(AppDbContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            IEnumerable<Item> itemsList = _db.Items.Include(c => c.Category).ToList();
            return View(itemsList);
        }

        //Get
        public IActionResult New()
        {
            CreateSelectList();
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New(Item item)
        {
            if(item.Price > 100)
            {
                ModelState.AddModelError("CustomError", "price can't be above 100");

                //custom error is Price or any item. value to get the error above and under the label with a red color
                //ModelState.AddModelError("Price", "price can't be above 100");
            }

            if (item.CategoryId == 1)
            {
                ModelState.AddModelError("CustomError", "invalid category");
            }
            if (ModelState.IsValid)
            {
                _db.Items.Add(item);
                _db.SaveChanges();
                TempData["successData"] = "Item has been added successfully";
                return RedirectToAction("Index");
            } else
            {
                return View(item);
            }
            
        }

        // GET
        public IActionResult Edit(int? Id)
        {

            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var item = GetItem(Id);
            CreateSelectList(item.CategoryId);
            return View(item);
        }

        private Item? GetItem(int? Id)
        {
            return _db.Items.Find(Id);
        }

        // put
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Item item)
        {
            if (item.Price > 100)
            {
                ModelState.AddModelError("CustomError", "price can't be above 100");

                //custom error is Price or any item. value to get the error above and under the label with red color
                //ModelState.AddModelError("Price", "price can't be above 100");
            }
            if (item.CategoryId == 1)
            {
                ModelState.AddModelError("CustomError", "invalid category");
            }
            if (ModelState.IsValid)
            {
                _db.Items.Update(item);
                _db.SaveChanges();
                TempData["successData"] = "Item has been edited successfully";
                return RedirectToAction("Index");
            }
            else
            {
                return View(item);
            }

        }

        // GET
        public IActionResult Delete(int? Id)
        {

            if (Id == null || Id == 0)
            {
                return NotFound();
            }
            var item = GetItem(Id);
            CreateSelectList(item.CategoryId);
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteItem(int? Id)
        { 
            var item = GetItem(Id);
            if(item == null)
            {
                return NotFound();
            }
            _db.Items.Remove(item);
            _db.SaveChanges();
            TempData["successData"] = "Item has been deleted successfully";
            return RedirectToAction("Index");
        }

        public void CreateSelectList (int selectId = 1)
        {
            //List<Category> categories = _db.Categories.ToList();
            IEnumerable<Category> categories = _db.Categories.ToList();
            ViewBag.Categoriylist = new SelectList(categories, "Id", "Name", selectId);
        }
    }
}

