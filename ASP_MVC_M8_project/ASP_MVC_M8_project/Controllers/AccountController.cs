using ASP_MVC_M8_project.Models;
using ASP_MVC_M8_project.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;

namespace ASP_MVC_M8_project.Controllers
{
    public class AccountController : Controller
    {
        // GET: Acc
        [AllowAnonymous]
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult Login(UserViewModel user)
        {
            using (var context = new EventDbcontext())
            {
                bool Isvalid = context.tblUsers.Any(u => u.UserName == user.UserName && u.Password == user.Password);
                if (Isvalid)
                {
                    FormsAuthentication.SetAuthCookie(user.UserName, false);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Username and password");
                    return View();
                }
            }

        }

        public ActionResult Logout()
        {
            //Session.Abandon();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login");
        }
    }
}