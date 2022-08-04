using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZAFMC.Controllers
{
    public class PassoversController : Controller
    {
        // GET: Passovers
        public ActionResult Index()
        {
            return View();
        }

        // GET: Passovers/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Passovers/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Passovers/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Passovers/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Passovers/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Passovers/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Passovers/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
