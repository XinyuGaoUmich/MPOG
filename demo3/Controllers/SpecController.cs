using System;
using System.Collections.Generic;
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
            //List<Pager_Auth_Result> pager_auth = db2.Pager_Auth(id).ToList();
            List<Spec_Result> spec_Results = db2.Spec(id).ToList();
            var nQS_Domain = db2.Enumeration_NQS_Domain;
            var measure_Type = db2.Enumeration_Measure_Type;
            var scope = db2.Enumeration_Scope;
            var responsible_provider = db2.Enumeration_Responsible_Provider;
            var collations = db2.Collations(id).ToList();
            var data_diagnostic = db2.Data_Diagnostics_Affected(id).ToList();
            var mpog_concept_header = db2.MPOG_Concept_ID_Required(id).ToList();
            var all_concept_ids = db2.Concept_Each_Header;
            var all_concepts = db2.MPOG_Concepts;
            //List<Pager_Result> pager = db2.Pager(id).ToList();
            List<Spec_Published_Result> spec_Published_Results = db2.Spec_Published(id).ToList();
            //var model = new EditPager { pager_Auth_Results = pager_auth, pager_Results = pager, nQS_Domain = nQS_Domain, measure_Type = measure_Type, scope = scope, responsible_Provider = responsible_provider };
            var model = new EditSpec { spec_Results = spec_Results, spec_Published_Results = spec_Published_Results, collations_Results = collations, concept_ID_Required_Results = mpog_concept_header, data_Diagnostics_Affected_Results = data_diagnostic, measure_Type = measure_Type, nQS_Domain = nQS_Domain, responsible_Provider = responsible_provider, scope = scope, all_Concept_ids = all_concept_ids, all_Concepts = all_concepts };
            return View(model);           
        }

        public ActionResult Edit(int? id)
        {
            if (Session["roles"] == null || !Session["roles"].ToString().Contains("MeasureSpecEditor"))
            {
                return RedirectToAction("LoginWithoutAccess","NoAccess");
            }
            //var spec_unpub = db2.Spec(id).
            return View();
        }

    }
}