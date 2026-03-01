using Library.Data;
using Library.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library.Data;
using Library.Models;

namespace Library.Controllers
{
    public class CheckoutController : Controller
    {
        private readonly LibraryContext _context;

        public CheckoutController(LibraryContext context)
        {
            _context = context;
        }

        // GET: CheckoutController
        public ActionResult Index()
        {
            return View();
        }

        // GET: CheckoutController/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var checkout = await _context.Checkouts
                .Include(c => c.Book)   // إذا كنت بحاجة لمعلومات الكتاب
                .Include(c => c.Member) // إذا كنت بحاجة لمعلومات العضو
                .FirstOrDefaultAsync(c => c.CheckoutId == id);

            if (checkout == null)
            {
                return NotFound(); // إذا لم يتم العثور على الاستعارة
            }

            return View(checkout); // عرض تفاصيل الاستعارة
        }

        // GET: CheckoutController/Create
        //[Authorize(Roles = "User")]
        public ActionResult Create()
        {
            ViewBag.BooksList = new SelectList(_context.Books.Where(b => b.AvailabilityBookStatus == true), "BookId", "Title");
            ViewBag.MembersList = new SelectList(_context.Members, "MemberId", "Name");
            return View();
        }

        // POST: CheckoutController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        //[Authorize(Roles = "User")]
        public async Task<IActionResult> Create(Checkout checkout)
        {
            if (ModelState.IsValid)
            {
                checkout.CheckoutDate = DateTime.Now;
                var book = await _context.Books.FindAsync(checkout.BookId);

                // تأكيد أن الكتاب متاح
                if (book.AvailabilityBookStatus == false)
                {
                    return View("Error", new ErrorViewModel { Message = "The book is not available." });

                }

                // تحديث حالة الكتاب إلى غير متاح
                book.AvailabilityBookStatus = false;

                // حفظ عملية الاستعارة
                _context.Checkouts.Add(checkout);
                await _context.SaveChangesAsync();

                return RedirectToAction("Index");
            }
            return View(checkout);
        }

        // GET: CheckoutController/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var checkout = await _context.Checkouts.FindAsync(id);
            if (checkout == null)
            {
                return NotFound();
            }
            ViewBag.BooksList = new SelectList(_context.Books.Where(b => b.AvailabilityBookStatus == true || b.BookId == checkout.BookId), "BookId", "Title");
            ViewBag.MembersList = new SelectList(_context.Members, "MemberId", "Name");
            return View(checkout);
        }


        // POST: CheckoutController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Checkout checkout)
        {
            if (id != checkout.CheckoutId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(checkout);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CheckoutExists(checkout.CheckoutId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
            }

            ViewBag.BooksList = new SelectList(_context.Books.Where(b => b.AvailabilityBookStatus == true || b.BookId == checkout.BookId), "BookId", "Title");
            ViewBag.MembersList = new SelectList(_context.Members, "MemberId", "Name");
            return View(checkout);
        }

        private bool CheckoutExists(int id)
        {
            return _context.Checkouts.Any(e => e.CheckoutId == id);
        }

        // GET: CheckoutController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: CheckoutController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        
    }
}
