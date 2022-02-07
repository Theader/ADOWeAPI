using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Controllers
{
    public class ReparteController : Controller
    {
        // GET: ReparteController
        public ActionResult Index()
        {
            return View();
        }

        // GET: ReparteController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ReparteController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReparteController/Create
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

        // GET: ReparteController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ReparteController/Edit/5
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

        // GET: ReparteController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ReparteController/Delete/5
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
