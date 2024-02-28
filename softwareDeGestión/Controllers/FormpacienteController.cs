using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace softwareDeGestión.Controllers
{
    public class FormpacienteController : Controller
    {
        // GET: FormpacienteController
        public ActionResult Index()
        {
            return View();
        }

        // GET: FormpacienteController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: FormpacienteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: FormpacienteController/Create
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

        // GET: FormpacienteController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: FormpacienteController/Edit/5
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

        // GET: FormpacienteController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: FormpacienteController/Delete/5
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
