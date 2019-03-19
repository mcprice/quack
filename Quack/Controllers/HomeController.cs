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
        public ActionResult Index()
        {
            var model = new ReportModel();

            DataTable data = model.GetAllFeedings();

            ViewBag.Feedings = data;

            return View();
        }
    }
}