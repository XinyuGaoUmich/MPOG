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

            //ViewBag.basic = db2.Pager_Auth(id).First();        
            //ViewBag.published = db2.Pager(id).First();
            //ViewBag.responsible_provider = db2.Enumeration_Responsible_Provider;
            //ViewBag.responsible_provider_id = db2.Responsible_Provider_Unpublished.Where(o => o.Measure_ID == id);

            List<Pager_Auth_Result> pager_auth = db2.Pager_Auth(id).ToList();
            var nQS_Domain = db2.Enumeration_NQS_Domain;
            var measure_Type = db2.Enumeration_Measure_Type;
            var scope = db2.Enumeration_Scope;
            var responsible_provider = db2.Enumeration_Responsible_Provider;         
            List<Pager_Result> pager = db2.Pager(id).ToList();
            var model = new EditPager { pager_Auth_Results = pager_auth, pager_Results = pager, nQS_Domain = nQS_Domain, measure_Type = measure_Type, scope = scope, responsible_Provider = responsible_provider };
            return View(model);
        }

        // GET: Pager/Edit/5
        public ActionResult Edit(int id)
        {
            if (Session["roles"] == null || !Session["roles"].ToString().Contains("MeasureSpecEditor"))
            {
                return RedirectToAction("LoginWithoutAccess", "NoAccess");
            }

            List<Pager_Auth_Result> pager_auth = db2.Pager_Auth(id).ToList();            
            var nQS_Domain = db2.Enumeration_NQS_Domain;           
            var measure_Type = db2.Enumeration_Measure_Type;           
            var scope = db2.Enumeration_Scope;
            var responsible_provider = db2.Enumeration_Responsible_Provider;         
            List<Pager_Result> pager = db2.Pager(id).ToList();
            var model = new EditPager { pager_Auth_Results = pager_auth, pager_Results = pager, nQS_Domain = nQS_Domain, measure_Type = measure_Type, scope = scope, responsible_Provider = responsible_provider };
            return View(model);
        }

      
        
        
        public JsonResult AutocompleteProvider(string term)
        {
            try
            {
                var providerList = db2.Enumeration_Responsible_Provider.Where(o => o.Responsible_Provider_Name.ToUpper().Contains(term.ToUpper())).Select(o => new { id = o.Responsible_Provider_ID, provider = o.Responsible_Provider_Name }).Distinct().ToList();
                return Json(new {
                    sucess = true,
                    message = "success",
                    providerList
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new
                {
                    success = false,
                    message = e.Message
                });
            }
           
        }

        [ValidateInput(false)]
        public JsonResult Save(int measure_id, string measure_abbreviation, int? nqs_domain, int? measure_type, int? scope, decimal threshold, string data_collection_method, string description, string measure_summary, string inclusions, string exclusions, string success, string risk_adjustment, string references, int provider, string new_provider)
        {
            try
            {                             
                if (provider > 0)
                {
                    db2.Save_Pager(measure_id, measure_abbreviation, data_collection_method, description, nqs_domain, measure_type, scope, measure_summary, inclusions, exclusions, success, threshold, provider, risk_adjustment, references);
                } 

                db2.Save_Pager(measure_id, measure_abbreviation, data_collection_method, description, nqs_domain, measure_type, scope, measure_summary, inclusions, exclusions, success, threshold, null, risk_adjustment, references);

                if (provider == -1 && new_provider != "")
                {
                    db2.Add_New_Provider(measure_id, new_provider);
                }
                return Json(new
                {
                    success = true,
                    message = "success"
                });


            }
            catch (Exception e)
            {
                return Json(new
                {
                    success = false,
                    message = e.Message
                });
            }
           
        }
      
        public JsonResult FindProvider (int id)
        {
           try
            {
                var provider = db2.Enumeration_Responsible_Provider.Where(o => o.Responsible_Provider_ID == id).First().Responsible_Provider_Name;
                return Json(new
                {
                    success = true,
                    provider
                }, JsonRequestBehavior.AllowGet);
            }
            catch (Exception e)
            {
                return Json(new
                {
                    success = false,
                    message = e.Message
                });
            }
        }

        //default parameter is id
        public ActionResult ModifyProvider(int? measure, int? provider)
        {        
            var published = db2.Measure_Of_Provider_Published(provider).ToList();
            var unpublished = db2.Measure_Of_Provider_Unpublished(provider).ToList();
            ViewBag.provider_id = provider;
            ViewBag.measure_ID = measure;
            ViewBag.measure_Name = db2.Pager_Auth(measure).Select( o => o.Measure_Abbreviation).First();
            ViewBag.provider_content = db2.Enumeration_Responsible_Provider.Where(o => o.Responsible_Provider_ID == provider).Select(o => o.Responsible_Provider_Name).First();
            var model = new ModifyProvider { unpublished_Providers = unpublished, published_Providers = published };
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Save_all_provider(int? provider_id, string provider_content)
        {
            try
            {
                db2.Edit_Provider(provider_id, provider_content);
                return Json(
                    new
                    {
                        success = true,
                        message = "success"
                    });
            }
            catch(Exception e)
            {
                return Json(new
                {
                    sucess = false,
                    message = e.Message
                });
            }
        }

    }
}
