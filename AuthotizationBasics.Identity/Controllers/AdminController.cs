using AuthotizationBasics.Identity.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace AuthotizationBasics.Identity.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public AdminController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Policy = "admin")]
        public IActionResult Administrator()
        {
            return View();
        }

        [AllowAnonymous]
        public IActionResult Login(string returnUrl)
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> LoginAsync(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            //var user = _context.Users.SingleOrDefault(u => u.UserName == model.UserName && u.Password == model.Password);
            var user = await _userManager.FindByNameAsync(model.UserName);

            if (user == null)
            {
                ModelState.AddModelError("UserName", "User not found");
                return View(model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, false);

            if (result.Succeeded)
                return Redirect(model.ReturnUrl);
            
            
            
            return View(model);

            //var claims = new List<Claim> { new Claim(ClaimTypes.Name, model.UserName) };
            //if (model.UserName =="admin")
            //{
            //    claims.Add(new Claim(ClaimTypes.Role, "administrator"));
            //}
            //var claimsIdentity = new ClaimsIdentity(claims, "Cookie");
            //var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            //await HttpContext.SignInAsync("Cookie", claimsPrincipal);


        }
    }

    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string ReturnUrl { get; set; }
    }
}
