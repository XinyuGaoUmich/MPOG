using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace demo3.Controllers
{
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }


        [Authorize]
        public ActionResult Login()
        {

            // session handling
            Auth auth = new Auth();
            //store info to session

            Session["userid"] = auth.getUserID();
            Session["first_name"] = auth.getFirstName();
            Session["last_name"] = auth.getLastName();
            Session["roles"] = auth.getRoles();
            
            return Redirect("/Measures/Index");
        }


        [Authorize]
        public ActionResult Logout()
        {

            /*
             System.Web.HttpContext.Current.Session["userid"] = null;
             System.Web.HttpContext.Current.Session["first_name"] = null;
             System.Web.HttpContext.Current.Session["last_name"] = null;
             System.Web.HttpContext.Current.Session["roles"] = null;
             */
            Request.GetOwinContext().Authentication.SignOut();
            //Request.GetOwinContext().Authentication.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            
            //Request.GetOwinContext().Authentication.SignOut(new Microsoft.Owin.Security.AuthenticationProperties
            //{
                //RedirectUri = Url.Action("Index", "Measures")
            //});
            Session.Abandon();
            return this.Redirect("/Measures/Index");
        }

        
    }
}

