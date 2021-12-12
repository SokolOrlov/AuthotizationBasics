using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace AuthotizationBasics.Base.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {

            ViewBag.Name = User.Identity.Name;
            ViewBag.IsAuthenticated = User.Identity.IsAuthenticated;
            return View();
        }

        public async Task<IActionResult> LogoffAsync()
        {

           await HttpContext.SignOutAsync();

            return Redirect("/home/index");
        }
    }
}
