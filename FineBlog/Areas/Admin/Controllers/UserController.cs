using AspNetCoreHero.ToastNotification.Abstractions;
using FineBlog.Models;
using FineBlog.ViewModel;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FineBlog.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly INotyfService _notification;
        public UserController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            INotyfService notyfService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _notification = notyfService;

        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("Login")]
        public IActionResult Login()
        {
            return View(new LoginVM());
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginVM vm)
        {
            if (!ModelState.IsValid) { return View(vm); }
            var existingUser = await _userManager.Users.FirstOrDefaultAsync(x => x.UserName == vm.Username);
            if (existingUser == null)
            {
                _notification.Error("Username does not exist");
                return View(vm);
            }
            var verifyPassword = await _userManager.CheckPasswordAsync(existingUser, vm.Password);
            if(!verifyPassword)
            {
                _notification.Error("Password does not match");
                return View(vm);
            }
            await _signInManager.PasswordSignInAsync(vm.Username, vm.Password, vm.RememberMe, true);
            _notification.Error("Login Succesful");
            return RedirectToAction("Index", "User", new {area="Admin"});
        }
    }
}
