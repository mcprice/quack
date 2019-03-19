using Quack.Models;
using System;
using System.Collections.Generic;
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
        public JsonResult Create(FormCollection collection)
        {
            try
            {
                string email = collection["email"];
                string name = collection["name"];
                string pass = collection["password"];

                var model = new ReportModel();

                int rowsEffected = model.Feed();

                if (rowsEffected > 0)
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
    }
}
