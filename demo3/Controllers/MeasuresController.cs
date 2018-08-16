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

        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public JsonResult CreateSave(string measure_abbreviation, string measure_title, string nqs_domain, string qcdr_measure_name, string vbr, string clinical_lead, string developer, string date_published, string status_id)
        {          
            try
            {
                //db.Measure_Site.Add(measure_Site);
                int? nqs_domain_int = null;
                if (nqs_domain != "")
                {
                   nqs_domain_int = Convert.ToInt32(nqs_domain);
                }

                bool? vbr_bool = null;
                if (vbr != "")
                {
                    vbr_bool = Convert.ToBoolean(vbr);
                }

                //var date = Convert.ToDateTime(date_published);
                int? status_id_int = null;
                if (status_id != "")
                {
                    status_id_int = Convert.ToInt32(status_id);
                }

                measure_title = measure_title == "" ? null : measure_title;
                qcdr_measure_name = qcdr_measure_name == "" ? null : qcdr_measure_name;
                clinical_lead = clinical_lead == "" ? null : clinical_lead;
                developer = developer == "" ? null : developer;
                db2.Add_Measure(measure_abbreviation, measure_title, nqs_domain_int, qcdr_measure_name, vbr_bool, clinical_lead, developer, null, null, status_id_int);
                return Json(new
                {
                    success = true,
                    message = "success",
                });
            } catch (Exception e)
            {
                return Json(new
                {
                    success = false,
                    message = e.Message,
                });
            }

           
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
        public JsonResult EditSave(string measure_id, string measure_abbreviation, string measure_title, string nqs_domain, string qcdr_measure_name, string vbr, string clinical_lead, string developer, string date_published, string status_id)
        {         
            try
            {
                int measure_id_int = Convert.ToInt32(measure_id);
                int? nqs_domain_int = null;
                if (nqs_domain != "")
                {
                    nqs_domain_int = Convert.ToInt32(nqs_domain);
                }
                bool? vbr_bool = null;
                if (vbr != "")
                {
                    vbr_bool = Convert.ToBoolean(vbr);
                }
                int? status_id_int = null;
                if (status_id != "")
                {
                    status_id_int = Convert.ToInt32(status_id);
                }
                measure_title = measure_title == "" ? null : measure_title;
                qcdr_measure_name = qcdr_measure_name == "" ? null : qcdr_measure_name;
                clinical_lead = clinical_lead == "" ? null : clinical_lead;
                developer = developer == "" ? null : developer;
                db2.Edit_Measure(measure_id_int, measure_abbreviation, measure_title, nqs_domain_int, qcdr_measure_name, vbr_bool, clinical_lead, developer, null, null, status_id_int);
                return Json(new
                {
                    success = true,
                    message = "success",
                });

            } catch (Exception e)
            {
                return Json(new
                {
                    success = false,
                    message = e.Message,
                });
            }
            
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
