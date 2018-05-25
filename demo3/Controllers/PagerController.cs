using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace demo3.Controllers
{
    public class PagerController : Controller
    {
        // GET: Pager
        public ActionResult Index()
        {
            return View();
        }

        // GET: Pager/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Pager/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Pager/Create
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

        // GET: Pager/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Pager/Edit/5
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

        // GET: Pager/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Pager/Delete/5
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
