using EmpsManagement.DAL.Models.Identity;
using EmpsManagement.PL.ViewModels.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmpsManagement.PL.Controllers.Identity
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        #region Register

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName
                };
                var result = _userManager.CreateAsync(user, model.Password).Result;
                if (result.Succeeded)
                {
                    // Optionally, you can sign in the user or redirect to a confirmation page
                    return RedirectToAction("LogIn");
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        #endregion

        #region LogIn

        [AllowAnonymous]
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        public IActionResult Login(LoginViewModel model , string? returnUrl = null)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var User = _userManager.FindByEmailAsync(model.Email).Result;

            if (User is not null)
            {
                bool isConfirm = _userManager.CheckPasswordAsync(User, model.Password).Result;

                if (isConfirm)
                {

                    var result = _signInManager.PasswordSignInAsync(User, model.Password, model.RememberMe, false).Result;
                    if (result.Succeeded)
                    {
                        if (result.IsNotAllowed)
                        {
                            ModelState.AddModelError(string.Empty, "You are not allowed to sign in.");
                        }

                        if (result.IsLockedOut)
                        {
                            ModelState.AddModelError(string.Empty, "Your account is locked out.");
                        }

                        if (result.Succeeded)
                        {
                            // Redirect to the home page or any other page after successful login
                            return RedirectToAction("Index", "Home");

                            //return Redirect(returnUrl ?? "/");

                        }
                    }

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                }

            }
                return View(model);
        }


        #endregion

        #region Logout

        [HttpPost]
        public async Task<IActionResult> Logout()
        {

            await _signInManager.SignOutAsync();
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            Response.Cookies.Delete(".AspNetCore.Identity.Application");
            await HttpContext.SignOutAsync(IdentityConstants.ApplicationScheme);



            return RedirectToAction("Login", "Account");
        }

        #endregion

    }
}
