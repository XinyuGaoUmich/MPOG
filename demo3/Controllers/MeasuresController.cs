using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using demo3.Models;

namespace demo3.Controllers
{
    public class MeasuresController : Controller
    {
        private MPOG_XinyuEntities3 db = new MPOG_XinyuEntities3();
        private MPOG_XinyuEntities4 db2 = new MPOG_XinyuEntities4();
      
        // GET: Measures
        public ActionResult Index()
        {
            if (Session["roles"] != null && Session["roles"].ToString().Contains("MeasureSpecEditor"))
            {
                
                return View(db2.Measure_List().ToList());
            }

                    
            return Redirect("/Measures/Public");
        }

        public ActionResult Public()
        {
            return View(db2.Public_Measure().ToList());
        }

        // GET: Measures/Details/5
        public ActionResult Details(int? id)
        {
            if (Session["userid"] == null)
            {
                return Redirect("/NoAccess/Index");
            }        


            if (Session["roles"] == null || !Session["roles"].ToString().Contains("MeasureSpecEditor"))
            {
                return Redirect("/NoAccess/LoginWithoutAccess");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            //Measure_Site measure_Site = db.Measure_Site.Find(id);
            List<Details_All_Result> a =  db2.Details_All(id).ToList();          
            
            if (a == null)
            {
                return HttpNotFound();
            }
            return View(a);
        }

        // GET: Measures/Create
        public ActionResult Create()
        {
            if (Session["userid"] == null)
            {
                return Redirect("/NoAccess/Index");
            }

            if (Session["roles"] == null || !Session["roles"].ToString().Contains("MeasureSpecEditor"))
            {
                return Redirect("/NoAccess/LoginWithoutAccess");
            }
            
            return View();
        }

        // POST: Measures/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Create([Bind(Include = "Measure_Abbreviation, Measure_Title, NQS_Domain, Measure_Name, VBR, Clinical_Lead, Developer, Measure_Spec_Completed, Date_Published")] Measure_Site measure_Site)
        public ActionResult Create(Measure_List_Result mea_List)
        {
            if (Session["userid"] == null)
            {
                return Redirect("/NoAccess/Index");
            }

            if (Session["roles"] == null || !Session["roles"].ToString().Contains("MeasureSpecEditor"))
            {
                return Redirect("/NoAccess/LoginWithoutAccess");
            }

            if (ModelState.IsValid)
            {
                //db.Measure_Site.Add(measure_Site);
                db2.Add_Measure(mea_List.Measure_Abbreviation, mea_List.Measure_Title, mea_List.NQS_Domain, mea_List.QCDR_Measure_Name, mea_List.VBR, mea_List.Clinical_Lead, mea_List.Developer, mea_List.Measure_Spec_Completed, mea_List.Date_Published);
                db2.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mea_List);
        }

        // GET: Measures/Edit/5
        public ActionResult Edit(int? id)
        {
            if (Session["userid"] == null)
            {
                return Redirect("/NoAccess/Index");
            }

            if (Session["roles"] == null || !Session["roles"].ToString().Contains("MeasureSpecEditor"))
            {
                return Redirect("/NoAccess/LoginWithoutAccess");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ASPIRE_Measures aSPIRE_Measures = db.ASPIRE_Measures.Find(id);
            if (aSPIRE_Measures == null)
            {
                return HttpNotFound();
            }
            return View(aSPIRE_Measures);
        }

        // POST: Measures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Measure_ID,Measure_Name,Measure_Collation,Measure_Description,Report_ID,Galileo_Measure_Name,Image_File_Name,Threshold,CMS_Measure_Number,Is_Published,Domain")] ASPIRE_Measures aSPIRE_Measures)
        {
            if (Session["userid"] == null)
            {
                return Redirect("/NoAccess/Index");
            }


            if (Session["roles"] == null || !Session["roles"].ToString().Contains("MeasureSpecEditor"))
            {
                return Redirect("/NoAccess/LoginWithoutAccess");
            }

            if (ModelState.IsValid)
            {
                db.Entry(aSPIRE_Measures).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(aSPIRE_Measures);
        }

        // GET: Measures/Delete/5
        public ActionResult Delete(int? id)
        {
            if (Session["userid"] == null)
            {
                return Redirect("/NoAccess/Index");
            }

            if (Session["roles"] == null || !Session["roles"].ToString().Contains("MeasureSpecEditor"))
            {
                return Redirect("/NoAccess/LoginWithoutAccess");
            }

            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            // ASPIRE_Measures aSPIRE_Measures = db.ASPIRE_Measures.Find(id);
            List<Details_All_Result> a = db2.Details_All(id).ToList();
            if (a == null)
            {
                return HttpNotFound();
            }
            return View(a);
        }

        // POST: Measures/Delete/5
        [HttpPost, ActionName("Delete")]
       // [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            if (Session["userid"] == null)
            {
                return Redirect("/NoAccess/Index");
            }

            if (Session["roles"] == null || !Session["roles"].ToString().Contains("MeasureSpecEditor"))
            {
                return Redirect("/NoAccess/LoginWithoutAccess");
            }

            // ASPIRE_Measures aSPIRE_Measures = db.ASPIRE_Measures.Find(id);
            //db.ASPIRE_Measures.Remove(aSPIRE_Measures);
            db2.Delete_Measure(id);
            db2.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        public ActionResult Pager(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section_fields section_Fields = db.Section_fields.Find(id);
            if (section_Fields == null)
            {
                return HttpNotFound();
            }
            return View(section_Fields);
        }

        public ActionResult Spec(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Section_fields section_Fields = db.Section_fields.Find(id);
            if (section_Fields == null)
            {
                return HttpNotFound();
            }
            return View(section_Fields);
        }

        public ActionResult Logout()
        {
            return Redirect("/Home/Logout");
        }

    }
}
