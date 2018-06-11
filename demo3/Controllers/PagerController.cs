using demo3.Models;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.tool.xml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace demo3.Controllers
{
    public class PagerController : Controller
    {
        // GET: Pager
        private MPOG_XinyuEntities4 db2 = new MPOG_XinyuEntities4();
        public ActionResult Index(int? id)
        {
            
            ViewBag.basic = db2.Pager_Auth(id).First();        
            ViewBag.published = db2.Pager(id).First();
            return View();
        }

        // GET: Pager/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["roles"] == null || !Session["roles"].ToString().Contains("MeasureSpecEditor"))
            {
                return RedirectToAction("LoginWithoutAccess", "NoAccess");
            }

            List<Pager_Auth_Result> pager_auth = db2.Pager_Auth(id).ToList();
            var nQS_Domain = db2.Enumerations.Where(o => o.Section_ID == 4);
            var measure_Type = db2.Enumerations.Where(o => o.Section_ID == 5);
            var scope = db2.Enumerations.Where(o => o.Section_ID == 6);
            List<Pager_Result> pager = db2.Pager(id).ToList();
            var model = new EditPager { pager_Auth_Results = pager_auth, pager_Results = pager, nQS_Domain = nQS_Domain, measure_Type = measure_Type, scope = scope };
            return View(model);
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
