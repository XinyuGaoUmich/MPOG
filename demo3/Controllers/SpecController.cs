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
            ViewBag.basic = db2.Spec(id).First();
            ViewBag.collations = db2.Collations(id);
            ViewBag.diagnostics = db2.Data_Diagnostics_Affected(id);
            ViewBag.concept = db2.MPOG_Concept_ID_Required(id);
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }

        public ActionResult ReturnToMeasure()
        {
            return Redirect("/Measures/Index");
        }
    }
}