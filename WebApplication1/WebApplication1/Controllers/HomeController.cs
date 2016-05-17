using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    /*
     * Home page view controller
     */ 
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        /*
        * Method: HTTP GET
        * URL: /About
        * Input: None
        * Output: About page
        */
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        /*
        * Method: HTTP GET
        * URL: /Contact
        * Input: None
        * Output: Contact page
        */
        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}