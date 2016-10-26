using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using HelperClasses;
using WebMatrix.WebData;
using System.Web.Security;

namespace VedicaAdmin.Controllers
{
    public class AccountController : Controller
    {
        //
        // GET: /Account/
        public ActionResult Login()
        {
            return View();
        }
        [Authorize]
        public ActionResult Home()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginCredential l)
        {
            if (!Membership.ValidateUser(l.UserName, l.Password))
            {
                return Redirect("/Account/Index?l=false");
            }
            else
            {
                WebSecurity.Login(l.UserName, l.Password);
                return RedirectToAction("Home", "Account");
            }
        }
        public ActionResult LogOut()
        {
            WebSecurity.Logout();
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
	}
}