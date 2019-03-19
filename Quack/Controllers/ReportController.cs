using Quack.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Quack.Controllers
{
    public class ReportController : Controller
    {
        public ActionResult NewFeeding()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Feed(FormCollection collection)
        {
            try
            {
                string location = collection["location"];
                string feedTime = collection["feedTime"];
                string numberFed = collection["numberFed"];
                string feedType = collection["feedType"];
                string gramsFed = collection["gramsFed"];

                var model = new ReportModel();

                int rowsEffected = model.Feed(location, feedTime, numberFed, feedType, gramsFed);

                if (rowsEffected == 1)
                {
                    return Json("1", JsonRequestBehavior.AllowGet);
                }

            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }

            return Json("0", JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult AllFeedings()
        {
            try
            {
                var model = new ReportModel();

                DataTable data = model.GetAllFeedings();

                ViewBag.Feedings = data;
            }
            catch (Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }

            return View();
        }
    }
}
