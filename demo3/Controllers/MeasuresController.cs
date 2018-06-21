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
                var measure_list = db2.Measure_List();
                var status_types = db2.Status_Type;            
                var model = new MeasureStatus { Measure_List_Results = measure_list, Status_Types = status_types };
                // return View(db2.Measure_List().ToList());
               
                return View(model);
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
            List<Details_All_Result> detail = db2.Details_All(id).ToList();
            //DetailsMetaData a = (DetailsMetaData)db2.Details_All(id); 
            
            //var nQS_Domain = db2.Enumerations.Where(o => o.Section_ID == 4);
            var nQS_Domain = db2.Enumeration_NQS_Domain;
            var status = db2.Status_Type;
            var model = new EditMeasure { Details_All_Results = detail, Status_Types = status, nQS_Domains = nQS_Domain };
            return View(model);
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

            //ViewBag.NQS_Domain = new SelectList(db2.Enumerations.Where(o => o.Section_ID == 4), "Enumeration_ID", "Enumeration_Content");
            ViewBag.NQS_Domain = new SelectList(db2.Enumeration_NQS_Domain, "NQS_Domain_ID", "NQS_Domain_Name");
            ViewBag.Status = new SelectList(db2.Status_Type, "Status_ID", "Status_Name");          
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
                db2.Add_Measure(mea_List.Measure_Abbreviation, mea_List.Measure_Title, mea_List.NQS_Domain, mea_List.QCDR_Measure_Name, mea_List.VBR, mea_List.Clinical_Lead, mea_List.Developer, mea_List.Measure_Spec_Completed, mea_List.Date_Published, mea_List.Status_ID);
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
            // ASPIRE_Measures aSPIRE_Measures = db.ASPIRE_Measures.Find(id);
            List<Details_All_Result> detail = db2.Details_All(id).ToList();
            if (detail == null)
            {
                return HttpNotFound();
            }
            // ViewBag.NQS_Domain = new SelectList(db2.Enumerations.Where(o => o.Section_ID == 4), "Enumeration_ID", "Enumeration_Content");
            //ViewBag.Status = new SelectList(db2.Status_Type, "Status_ID", "Status_Name");
            //return View(details_All_Results);
            //var nQS_Domain = db2.Enumerations.Where(o => o.Section_ID == 4);
            var status = db2.Status_Type;
            var nQS_Domain = db2.Enumeration_NQS_Domain;
            var model = new EditMeasure { Details_All_Results = detail, Status_Types = status, nQS_Domains = nQS_Domain };
            return View(model);
        }

        // POST: Measures/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Measure_ID,Measure_Name,Measure_Collation,Measure_Description,Report_ID,Galileo_Measure_Name,Image_File_Name,Threshold,CMS_Measure_Number,Is_Published,Domain")] ASPIRE_Measures aSPIRE_Measures)
        public ActionResult Edit(Details_All_Result details_All_Result)
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
                //db.Entry(aSPIRE_Measures).State = EntityState.Modified;
                db2.Edit_Measure(details_All_Result.Measure_ID, details_All_Result.Measure_Abbreviation, details_All_Result.Measure_Title, details_All_Result.NQS_Domain, details_All_Result.QCDR_Measure_Name, details_All_Result.VBR,details_All_Result.Clinical_Lead, details_All_Result.Developer, details_All_Result.Measure_Spec_Completed, details_All_Result.Date_Published,details_All_Result.Status_ID);
                db2.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(details_All_Result);
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
            return RedirectToAction("Index", "Pager", new { id});
        }

        public ActionResult Spec(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            return RedirectToAction("Index", "Spec", new { id });
        }

        public ActionResult Logout()
        {
            return Redirect("/Home/Logout");
        }

        [HttpPost]
        public JsonResult Update_Status( Update_status update_Status)
        {

            int result = db2.Update_Status(update_Status.record_id, update_Status.record_selection);            
            return Json("update measure status successfully!");

        }
      

    }
}
