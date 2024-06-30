using firstProject.Data;
using firstProject.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            IEnumerable<Models.Item> itemsList = _db.Items.ToList();
            return View(itemsList);
        }

        //Get
        public IActionResult New()
        {
            return View();
        }

        // POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult New(Models.Item item)
        {
            if(item.Price > 100)
            {
                ModelState.AddModelError("CustomError", "price can't be above 100");

                //custom error is Price or any item.value to get the error above and under the label with red color
                //ModelState.AddModelError("Price", "price can't be above 100");
            }

            if (item.CategoryId == 0)
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
            return View(item);
        }

        private Item? GetItem(int? Id)
        {
            return _db.Items.Find(Id);
        }

        // put
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(Models.Item item)
        {
            if (item.Price > 100)
            {
                ModelState.AddModelError("CustomError", "price can't be above 100");

                //custom error is Price or any item.value to get the error above and under the label with red color
                //ModelState.AddModelError("Price", "price can't be above 100");
            }
            if (item.CategoryId == 0)
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
    }
}

