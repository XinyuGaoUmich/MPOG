using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using demo3.Models;

namespace demo3.Controllers
{
    public class SpecController : Controller
    {
        private MPOG_XinyuEntities4 db2 = new MPOG_XinyuEntities4();
        // GET: Spec
        public ActionResult Index(int? id)
        {
            List<Spec_Result> spec_Results = db2.Spec(id).ToList();
            var nQS_Domain = db2.Enumeration_NQS_Domain;
            var measure_Type = db2.Enumeration_Measure_Type;
            var scope = db2.Enumeration_Scope;
            var responsible_provider = db2.Enumeration_Responsible_Provider;
            //var collations = db2.Collations(id).ToList();
            var data_diagnostic = db2.Data_Diagnostics_Affected(id).ToList();
            var mpog_concept_header = db2.MPOG_Concept_ID_Required(id).ToList();
            var all_concept_ids = db2.Concept_Each_Header.OrderBy(o => o.MPOG_Concept_ID);
            var all_concepts = db2.MPOG_Concepts;
            List<Spec_Published_Result> spec_Published_Results = db2.Spec_Published(id).ToList();
            var model = new EditSpec { spec_Results = spec_Results, spec_Published_Results = spec_Published_Results, collations_Results = null, concept_ID_Required_Results = mpog_concept_header, data_Diagnostics_Affected_Results = data_diagnostic, measure_Type = measure_Type, nQS_Domain = nQS_Domain, responsible_Provider = responsible_provider, scope = scope, all_Concept_ids = all_concept_ids, all_Concepts = all_concepts };
            return View(model);           
        }

        public ActionResult Edit(int? id)
        {
            if (Session["roles"] == null || !Session["roles"].ToString().Contains("MeasureSpecEditor"))
            {
                return RedirectToAction("LoginWithoutAccess","NoAccess");
            }
            List<Spec_Result> spec_Results = db2.Spec(id).ToList();
            var nQS_Domain = db2.Enumeration_NQS_Domain;
            var measure_Type = db2.Enumeration_Measure_Type;
            var scope = db2.Enumeration_Scope;
            var responsible_provider = db2.Enumeration_Responsible_Provider;
            //var collations = db2.Collations(id).ToList();
            var data_diagnostic = db2.Data_Diagnostics_Affected(id).ToList();
            var mpog_concept_header = db2.MPOG_Concept_ID_Required(id).ToList();
            var all_concept_ids = db2.Concept_Each_Header;
            var all_concepts = db2.MPOG_Concepts;
            List<Spec_Published_Result> spec_Published_Results = db2.Spec_Published(id).ToList();
            var model = new EditSpec { spec_Results = spec_Results, spec_Published_Results = spec_Published_Results, collations_Results = null, concept_ID_Required_Results = mpog_concept_header, data_Diagnostics_Affected_Results = data_diagnostic, measure_Type = measure_Type, nQS_Domain = nQS_Domain, responsible_Provider = responsible_provider, scope = scope, all_Concept_ids = all_concept_ids, all_Concepts = all_concepts };
            return View(model);
        }

        [HttpPost]
        public JsonResult ViewCollations (int? measure_id)
        {
            try
            {
                var collations = db2.Collations(measure_id).ToList();
                return Json(new
                {
                    success = true,
                    collations,
                });
            } catch (Exception e)
            {
                return Json(new {
                    success = false,
                    message = e.Message,
                });
            }
        }

        public JsonResult FindProvider(int id)
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


        [HttpPost]
        public JsonResult SaveNewDomain(int? measure_id, string new_domain)
        {
            try
            {
                db2.Add_New_Domain(measure_id, new_domain);
                return Json(new
                {
                    success = true,
                    message = "add new domain successfully!"
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

        [HttpPost]
        public JsonResult SaveNewMeasureType(int? measure_id, string new_measure_type)
        {
            try
            {
                db2.Add_New_Measure_Type(measure_id, new_measure_type);
                return Json(new
                {
                    success = true,
                    message = "add new measure type successfully!"
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

        [HttpPost]
        public JsonResult SaveNewMeasureScope(int? measure_id, string new_scope)
        {
            try
            {
                db2.Add_New_Measure_Scope(measure_id, new_scope);
                return Json(new
                {
                    success = true,
                    message = "add new measure scope successfully!"
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

        //default parameter is id
        public ActionResult ModifyProvider(int? measure, int? provider)
        {
            if (Session["roles"] == null || !Session["roles"].ToString().Contains("MeasureSpecEditor"))
            {
                return RedirectToAction("LoginWithoutAccess", "NoAccess");
            }

            var published = db2.Measure_Of_Provider_Published(provider).ToList();
            var unpublished = db2.Measure_Of_Provider_Unpublished(provider).ToList();
            ViewBag.provider_id = provider;
            ViewBag.measure_ID = measure;
            ViewBag.measure_Name = db2.Spec(measure).Select(o => o.Measure_Abbreviation).First();
            ViewBag.provider_content = db2.Enumeration_Responsible_Provider.Where(o => o.Responsible_Provider_ID == provider).Select(o => o.Responsible_Provider_Name).First();
            var model = new ModifyProvider { unpublished_Providers = unpublished, published_Providers = published };
            return View(model);
        }

        public ActionResult ModifyDomain(int? measure, int? domain)
        {
            if (Session["roles"] == null || !Session["roles"].ToString().Contains("MeasureSpecEditor"))
            {
                return RedirectToAction("LoginWithoutAccess", "NoAccess");
            }

            var published = db2.Measure_Of_Domain_Published(domain).ToList();
            var unpublished = db2.Measure_Of_Domain_Unpublished(domain).ToList();
            ViewBag.domain_id = domain;
            ViewBag.measure_ID = measure;
            ViewBag.measure_Name = db2.Spec(measure).Select(o => o.Measure_Abbreviation).First();
            ViewBag.domain_content = db2.Enumeration_NQS_Domain.Where(o => o.NQS_Domain_ID == domain).Select(o => o.NQS_Domain_Name).First();
            var model = new ModifyDomain { unpublished_Domains = unpublished, published_Domains = published };
            return View(model);
        }

        [HttpPost]
        public JsonResult DeleteDomain(int? measure, int? domain)
        {
            try
            {
                var published = db2.Measure_Of_Domain_Published(domain).ToList();
                var unpublished = db2.Measure_Of_Domain_Unpublished(domain).ToList();
                var domain_content = db2.Enumeration_NQS_Domain.Where(o => o.NQS_Domain_ID == domain).Select(o => o.NQS_Domain_Name).First();
                var unpublishedList = new List<string>();
                var publishedList = new List<string>();
                foreach (var item in unpublished)
                {
                    var itemName = db2.Spec_Published(item.Unpublished_Measure_ID).Select(o => o.Measure_Abbreviation).First();
                    unpublishedList.Add(itemName);
                }
                foreach (var item in published)
                {
                    var itemName = db2.Spec_Published(item.Published_Measure_ID).Select(o => o.Measure_Abbreviation).First();
                    publishedList.Add(itemName);
                }
                return Json(new
                {
                    success = true,
                    domain_id = domain,
                    unpublishedList,
                    publishedList
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

        [HttpPost]
        public JsonResult ConfirmDelete(int? domain_id)
        {
            try
            {
                db2.Delete_Domain(domain_id);
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

        public ActionResult ModifyMeasureType(int? measure, int? measure_type)
        {
            if (Session["roles"] == null || !Session["roles"].ToString().Contains("MeasureSpecEditor"))
            {
                return RedirectToAction("LoginWithoutAccess", "NoAccess");
            }

            var published = db2.Measure_Of_Measure_Type_Published(measure_type).ToList();
            var unpublished = db2.Measure_Of_Measure_Type_Unpublished(measure_type).ToList();
            ViewBag.measure_type_id = measure_type;
            ViewBag.measure_ID = measure;
            ViewBag.measure_Name = db2.Spec(measure).Select(o => o.Measure_Abbreviation).First();
            ViewBag.measure_type_content = db2.Enumeration_Measure_Type.Where(o => o.Measure_Type_ID == measure_type).Select(o => o.Measure_Type_Name).First();
            var model = new ModifyMeasureType { unpublished_Measure_Types = unpublished, published_Measure_Types = published };
            return View(model);
        }

        public ActionResult ModifyScope(int? measure, int? scope)
        {
            if (Session["roles"] == null || !Session["roles"].ToString().Contains("MeasureSpecEditor"))
            {
                return RedirectToAction("LoginWithoutAccess", "NoAccess");
            }

            var published = db2.Measure_Of_Scope_Published(scope).ToList();
            var unpublished = db2.Measure_Of_Scope_Unpublished(scope).ToList();
            ViewBag.scope_id = scope;
            ViewBag.measure_ID = measure;
            ViewBag.measure_Name = db2.Spec(measure).Select(o => o.Measure_Abbreviation).First();
            ViewBag.scope_content = db2.Enumeration_Scope.Where(o => o.Scope_ID == scope).Select(o => o.Scope_Name).First();
            var model = new ModifyScope { unpublished_Scopes = unpublished, published_Scopes = published };
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
            catch (Exception e)
            {
                return Json(new
                {
                    sucess = false,
                    message = e.Message
                });
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult Save_new_provider(int? measure_id, string new_provider)
        {
            try
            {
                db2.Add_New_Provider(measure_id, new_provider);
                return Json(
                    new
                    {
                        success = true,
                        message = "success"
                    });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    sucess = false,
                    message = e.Message
                });
            }
        }

        [HttpPost]
        public JsonResult Save_all_domain(int? domain_id, string domain_content)
        {
            try
            {
                db2.Edit_Domain(domain_id, domain_content);
                return Json(
                    new
                    {
                        success = true,
                        message = "success"
                    });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    sucess = false,
                    message = e.Message
                });
            }
        }

        [HttpPost]
        public JsonResult Save_new_domain(int? measure_id, string new_domain)
        {
            try
            {
                db2.Add_New_Domain(measure_id, new_domain);
                return Json(
                    new
                    {
                        success = true,
                        message = "success"
                    });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    sucess = false,
                    message = e.Message
                });
            }
        }

        [HttpPost]
        public JsonResult Save_all_measure_type(int? measure_type_id, string measure_type_content)
        {
            try
            {
                db2.Edit_Measure_Type(measure_type_id, measure_type_content);
                return Json(
                    new
                    {
                        success = true,
                        message = "success"
                    });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    sucess = false,
                    message = e.Message
                });
            }
        }

        [HttpPost]
        public JsonResult Save_new_measure_type(int? measure_id, string new_measure_type)
        {
            try
            {
                db2.Add_New_Measure_Type(measure_id, new_measure_type);
                return Json(
                    new
                    {
                        success = true,
                        message = "success"
                    });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    sucess = false,
                    message = e.Message
                });
            }
        }

        [HttpPost]
        public JsonResult Save_all_scope(int? scope_id, string scope_content)
        {
            try
            {
                db2.Edit_Measure_Scope(scope_id, scope_content);
                return Json(
                    new
                    {
                        success = true,
                        message = "success"
                    });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    sucess = false,
                    message = e.Message
                });
            }
        }

        [HttpPost]
        public JsonResult Save_new_scope(int? measure_id, string new_scope)
        {
            try
            {
                db2.Add_New_Measure_Scope(measure_id, new_scope);
                return Json(
                    new
                    {
                        success = true,
                        message = "success"
                    });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    sucess = false,
                    message = e.Message
                });
            }
        }

        public JsonResult AutocompleteConcept(string term)
        {
            try
            {
                //var providerList = db2.Enumeration_Responsible_Provider.Where(o => o.Responsible_Provider_Name.ToUpper().Contains(term.ToUpper())).Select(o => new { id = o.Responsible_Provider_ID, provider = o.Responsible_Provider_Name }).Distinct().ToList();
                var conceptList = db2.MPOG_Concepts.Where(o => o.concept_desc.ToUpper().Contains(term.ToUpper())).Select(o => new { id = o.MPOG_Concept_ID, concept = o.concept_desc }).Distinct().ToList();
                return Json(new
                {
                    sucess = true,
                    message = "success",
                    conceptList
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
        public JsonResult Save(int measure_id, string measure_abbreviation, int? nqs_domain, int? measure_type, int? scope, decimal threshold, string data_collection_method, string description, string measure_summary, string rationale, string inclusions, string exclusions, string other_measure_build_details, string success, string risk_adjustment, string references, int provider, string new_provider, Dictionary<int, string> existing_header_name, Dictionary<string, string> delete_existing_header, Dictionary<string, string> add_existing_header, Dictionary<string, string> newConceptHeaderTocontroller)
        {
            try
            {
                if (provider > 0)
                {
                    db2.Save_Spec(measure_id, measure_abbreviation, data_collection_method, description, nqs_domain, measure_type, scope, measure_summary, rationale, inclusions, exclusions, other_measure_build_details, success, threshold, provider, risk_adjustment, references);
                }

                db2.Save_Spec(measure_id, measure_abbreviation, data_collection_method, description, nqs_domain, measure_type, scope, measure_summary, rationale, inclusions, exclusions, other_measure_build_details, success, threshold, null, risk_adjustment, references);

                if (provider == -1 && new_provider != "")
                {
                    db2.Add_New_Provider(measure_id, new_provider);
                }
                bool ModifyHeaderName = (existing_header_name != null && existing_header_name.Count() != 0);
                bool DeleteConcept = (delete_existing_header != null && delete_existing_header.Count() != 0);
                bool AddConceptExistingHeader = (add_existing_header != null && add_existing_header.Count() != 0);
                bool AddConceptNewHeader = (newConceptHeaderTocontroller != null && newConceptHeaderTocontroller.Count() != 0);
                if (ModifyHeaderName) {
                    foreach(KeyValuePair<int, string> entry in existing_header_name)
                    {
                        db2.Modify_Header_Name(entry.Key, entry.Value);
                        
                    }
                }
                if (DeleteConcept)
                {
                    foreach (KeyValuePair<string, string> entry in delete_existing_header)
                    {                     
                        string headerIDstring = entry.Key;
                        if (headerIDstring == "empty")
                        {
                            break;
                        }

                        int headerId = Convert.ToInt32(headerIDstring);
                        string[] delete_concept_Id = entry.Value.Split(',');                    
                        for (int i = 0; i < delete_concept_Id.Length; i++)
                        {
                            int conceptId = Convert.ToInt32(delete_concept_Id[i]);
                            db2.Delete_Concept(headerId, conceptId);
                        }
                    }
                }
                if(AddConceptExistingHeader)
                {
                    foreach (KeyValuePair<string, string> entry in add_existing_header)
                    {
                        string headerstring = entry.Key;
                        if(headerstring == "empty")
                        {
                            break;
                        }
                        int headerId = Convert.ToInt32(headerstring);
                        string[] add_concept_Id = entry.Value.Split(',');
                        for (int i = 0; i < add_concept_Id.Length; i++)
                        {
                            int conceptId = Convert.ToInt32(add_concept_Id[i]);
                            db2.Add_Concept_Existing_Header(headerId, conceptId);
                        }
                    }
                }
                if (AddConceptNewHeader)
                {                   
                    foreach (KeyValuePair<string, string> entry in newConceptHeaderTocontroller)
                    {
                        
                        string headername = entry.Key; 
                        if (headername == "-1")
                        {
                            continue;
                        }
                        ObjectParameter headerId = new ObjectParameter("new_header_id", 0);
                        db2.Add_New_Header(measure_id, headername, headerId);
                        int headerIDtoint = Convert.ToInt32(headerId.Value);
                        if(entry.Value == null)
                        {
                            continue;
                        }
                        string[] conceptIds = entry.Value.Split(',');

                       
                        for (int i = 0; i < conceptIds.Length; i++)
                        {
                            if(conceptIds[i] == "")
                            {
                                continue;
                            }
                            int conceptID = Convert.ToInt32(conceptIds[i]);
                            db2.Add_Concept_Existing_Header(headerIDtoint, conceptID);
                        }
                    }
                }


                return Json(new
                {
                    success = true,
                    message = "success",
                    delete_existing_header,
                    add_existing_header
                });
            }
            catch (Exception e)
            {
                return Json(new
                {
                    success = false,
                    message = e.Message,
                  
                    newConceptHeaderTocontroller
                });
            }
        }

        [ValidateInput(false)]
        public JsonResult Publish(int measure_id, string measure_abbreviation, int? nqs_domain, string domain_content, int? measure_type, int? scope, decimal threshold, string data_collection_method, string description, string measure_summary, string rationale, string inclusions, string exclusions, string other_measure_build_details, string success, string risk_adjustment, string references, int provider, string new_provider)
        {
            try
            {
                if (provider > 0)
                {
                    db2.Publish_Spec(measure_id, measure_abbreviation, data_collection_method, description, nqs_domain, domain_content, measure_type, scope, measure_summary, rationale, inclusions, exclusions, other_measure_build_details, success, threshold, provider, risk_adjustment, references);
                }
                db2.Publish_Spec(measure_id, measure_abbreviation, data_collection_method, description, nqs_domain, domain_content, measure_type, scope, measure_summary, rationale, inclusions, exclusions, other_measure_build_details, success, threshold, null, risk_adjustment, references);
                if (provider == -1 && new_provider != "")
                {
                    db2.Add_New_Provider_Published(measure_id, new_provider);
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
    }
}