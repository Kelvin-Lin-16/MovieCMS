using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    public class HelloWorldController : Controller
    {
        /*
        * Method: HTTP GET
        * URL: /HelloWorld
        * Input: None
        * Output: HelloWorld page
        */
        public ActionResult Index()
        {
            return View();
        }
        
        /*
        * Method: HTTP GET
        * URL: /HelloWorld/Welcome
        * Input: None
        * Output: Welcome page
        */
        public ActionResult Welcome(string name = "Lin", int numTimes = 1)
        {
            int a = 100;
            int b = 200;
            int c = a * b;
            //Pass parameters to view page
            ViewBag.Message = "Welcome " + c.ToString() + Server.HtmlEncode(" " + name); //HttpUtility.HtmlEncode(" "+name);
            ViewBag.NumTimes = numTimes;
            return View();
        }
    }
}