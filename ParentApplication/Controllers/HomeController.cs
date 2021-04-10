﻿using System.Collections.Generic;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace ParentApplication.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                const string Issuer = "urn:net-desktop";
                var identity = new ClaimsIdentity(
                    new List<Claim>
                    {
                        new Claim(ClaimTypes.Email, model.Email, ClaimValueTypes.String, Issuer)
                    },
                    "Cookie",
                    ClaimTypes.Email,
                    ClaimTypes.Role
                    );
                var principal = new ClaimsPrincipal(identity);

                var ctx = Request.GetOwinContext();
                ctx.Authentication.SignIn(identity);
                Response.Cookies.Add(new HttpCookie("emailAddress","em.ashiq@gmail.com"));
                return RedirectToAction("Index");
            }
            else
            {
                return View("Index", model);
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Logout()
        {
            var ctx = Request.GetOwinContext();
            ctx.Authentication.SignOut("Cookie");
            return RedirectToAction("Index");
        }
    }
}