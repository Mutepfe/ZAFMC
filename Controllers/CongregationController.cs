using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZAFMC.Controllers
{
    public class CongregationController : Controller
    {
        // GET: Congregation
        public ActionResult Index()
        {
            
            return View();
        }

        // GET: Congregation/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Congregation/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Congregation/Create
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

        // GET: Congregation/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Congregation/Edit/5
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

        // GET: Congregation/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Congregation/Delete/5
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
