using Library.Models;
using Library.Data;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Library.Controllers
{
    public class OrderController : Controller
    {
        private readonly LibraryContext _db;

        // Constructor for dependency injection
        public OrderController(LibraryContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            var objOrderList = _db.Orders
                                   .Include(o => o.Member) // Ensure the Member is included
                                   .ToList(); // Execute the query and load the orders
            return View(objOrderList);
        }

        //GET
        public IActionResult Create()
        {
 
            return View();
        }
        //POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(Order obj)
        {
            if (ModelState.IsValid)
            {
                _db.Orders.Add(obj);
                _db.SaveChanges();

                return RedirectToAction("Index");
            }
            return View(obj);
        }

    }
}
