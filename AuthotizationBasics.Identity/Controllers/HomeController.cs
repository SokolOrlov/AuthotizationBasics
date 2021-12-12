using AuthotizationBasics.Identity.Entities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace AuthotizationBasics.Identity.Controllers
{
    public class HomeController : Controller
    {
        public HomeController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public UserManager<ApplicationUser> _userManager { get; }

        private readonly SignInManager<ApplicationUser> _signInManager;

        public IActionResult Index()
        {

            ViewBag.Name = User.Identity.Name;
            ViewBag.IsAuthenticated = User.Identity.IsAuthenticated;
            return View();
        }

        public async Task<IActionResult> LogoffAsync()
        {
            await _signInManager.SignOutAsync(); 

            return Redirect("/home/index");
        }
    }
}
