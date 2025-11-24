using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ParkWhereRest.Controllers
{
    public class ParkWhereController : Controller
    {
        // GET: ParkWhereController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ParkWhereController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ParkWhereController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ParkWhereController/Create
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

        // GET: ParkWhereController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ParkWhereController/Edit/5
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

        // GET: ParkWhereController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ParkWhereController/Delete/5
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
