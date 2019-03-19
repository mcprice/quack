using Quack.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Quack.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Retrieve the home page view.
        /// </summary>
        /// <returns>The rendered view tempalate</returns>
        public ActionResult Index()
        {
            return View();
        }
    }
}