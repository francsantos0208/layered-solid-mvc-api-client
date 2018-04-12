using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Website.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult HandleAllErrors()
        {
            return View();
        }
    }
}