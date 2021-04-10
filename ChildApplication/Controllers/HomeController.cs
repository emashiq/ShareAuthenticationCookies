using ChildApplication.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ChildApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            const string Issuer = "urn:net-core";
            var identity = new ClaimsIdentity(
                new List<Claim>
                {
                        new Claim(ClaimTypes.Email, "em.ashiq@gmail.com", ClaimValueTypes.String, Issuer)
                },
                ClaimTypes.Email,
                ClaimTypes.Role,
                "Cookie");
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync("Cookie", principal,
                new AuthenticationProperties
                {
                    ExpiresUtc = DateTime.UtcNow.AddMinutes(20),
                    IsPersistent = false,
                    AllowRefresh = false
                });
            HttpContext.Response.Cookies.Append("emailAddress","em.ashiq@gmail.com");
            //var result = await HttpContext.AuthenticateAsync("Cookie");
            return View();
        }

        public IActionResult Privacy()
        {

            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
