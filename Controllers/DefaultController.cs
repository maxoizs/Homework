using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using NewMVC.Models;

namespace NewMVC.Controllers
{
    public class DefaultController : Controller
    {
        //
        // GET: /Default/ ./ index
        public ActionResult Index()
        {
            return View("GameView"); 
        }

        public ActionResult Persons()
        {
            Person p = new  Person(){Name ="Stefan",Age=22,ismale= true};

            return Json(p, JsonRequestBehavior.AllowGet); 
            
        }

    }
}
