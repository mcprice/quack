using Quack.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Quack.Controllers
{
    public class UserController : Controller
    {
        // GET: User
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Login()
        {
            return View();
        }

        public ActionResult Logout()
        {
            Session.Clear();

            return View("Login");
        }

        // POST: User/Login
        [HttpPost]
        public JsonResult Login(FormCollection collection)
        {
            try
            {
                string email = collection["email"];

                var model = new UserModel();

                bool loggedIn = model.Login(email, collection["password"]);

                if (loggedIn)
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

        // POST: User/Create
        [HttpPost]
        public JsonResult Create(FormCollection collection)
        {
            try
            {
                string email = collection["email"];
                string name = collection["name"];
                string pass = collection["password"];

                var model = new UserModel();

                if (model.FindDuplicateEmail(email))
                {
                    return Json("2", JsonRequestBehavior.AllowGet);
                }

                int rowsEffected = model.Register(email, pass, name);

                if (rowsEffected > 0)
                {
                    return Json("1", JsonRequestBehavior.AllowGet);
                } 
                
            }
            catch(Exception e)
            {
                System.Diagnostics.Debug.WriteLine(e);
            }

            return Json("0", JsonRequestBehavior.AllowGet);
        }


    }
}
