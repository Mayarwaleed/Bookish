using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class PenaltyController : Controller
    {
        // GET: PenaltyController
        public ActionResult Index()
        {
            return View();
        }

        // GET: PenaltyController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: PenaltyController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PenaltyController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
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

        // GET: PenaltyController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: PenaltyController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
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

        // GET: PenaltyController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: PenaltyController/Delete/5
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
