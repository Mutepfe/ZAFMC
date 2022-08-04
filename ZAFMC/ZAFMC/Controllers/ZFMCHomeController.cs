using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ZAFMC.Controllers
{
    public class ZFMCHomeController : Controller
    {
        // GET: ZFMCHome
        public ActionResult Index()
        {
            
            return View();
        }

        // GET: ZFMCHome/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ZFMCHome/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ZFMCHome/Create
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

        // GET: ZFMCHome/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ZFMCHome/Edit/5
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

        // GET: ZFMCHome/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ZFMCHome/Delete/5
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
