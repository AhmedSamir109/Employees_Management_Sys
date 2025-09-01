using EmpsManagement.BLL.Services.EmailService;
using EmpsManagement.DAL.Models;
using EmpsManagement.DAL.Models.Identity;
using EmpsManagement.PL.ViewModels.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmpsManagement.PL.Controllers.Identity
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager , RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        #region Register

        
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

    
        [ValidateAntiForgeryToken]
        [HttpPost]
        public async  Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email.Split('@')[0],
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    IsAgree = model.IsAgree
                };
                var result = await _userManager.CreateAsync(user, model.Password);  // take password --> hash it then store user in DB
                if (result.Succeeded)
                {
                    // Assign role here
                    if (!await _roleManager.RoleExistsAsync("User"))
                        await _roleManager.CreateAsync(new IdentityRole("User"));

                    await _userManager.AddToRoleAsync(user, "User");

                    //await _signInManager.SignInAsync(user, isPersistent: false);


                    // Optionally, you can sign in the user or redirect to a confirmation page
                    return RedirectToAction("Login");
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

       
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }


        [ValidateAntiForgeryToken]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model , string? returnUrl = null)
        {

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            var User = await _userManager.FindByEmailAsync(model.Email);      //get user by email

            if (User is not null)
            {
                bool isConfirm = await  _userManager.CheckPasswordAsync(User, model.Password);  // check if the password is correct

                if (isConfirm)
                {

                    // log in the user with the provided password and  -->  generate token
                    var result = await _signInManager.PasswordSignInAsync(User, model.Password, model.RememberMe, false);  

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
            return RedirectToAction("Login", "Account");
        }

        #endregion

        #region ForgetPassword

        [HttpGet]
        public IActionResult ForgetPassword()
        {
            return View();
        }


        public async Task<IActionResult> SendEmail(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var User = await _userManager.FindByEmailAsync(model.Email); // get user by email
                if(User is not null)
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(User); // generate password reset token - valid for 1 hour by default
                   
                    var ResetPasswordBody = Url.Action("ResetPassword", "Account", new { email = model.Email, token = token }, Request.Scheme); // generate reset password link
                   
                    var email = new Email()
                    {
                        ToWhom = model.Email,
                        Subject = "Password Reset Request",
                        Body = ResetPasswordBody

                    };

                    EmailSender.SendEmail(email); // send email to the user
                    return RedirectToAction("CheckYourInbox"); // redirect to the CheckYourInbox view

                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Email Does't Exisits");
                }
            }

            return View("ForgetPassword", model);

        }


        public IActionResult CheckYourInbox()
        {
                       return View();
        }

        #endregion

        #region ResetPassword

        [HttpGet]
        public IActionResult ResetPassword(string email , string token)
        {
            TempData["email"] = email;
            TempData["token"] = token;

            return View();
        }


        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {

                var email = TempData["email"] as string;
                var token = TempData["token"] as string;


                var User = await _userManager.FindByEmailAsync(email);

                var result =await _userManager.ResetPasswordAsync(User, token, model.NewPassword); // reset password using the token
          
                if(result.Succeeded)
                {
                    // Optionally, you can sign in the user or redirect to a confirmation page
                    return RedirectToAction("Login");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }

            }
                return View(model);

        }
        #endregion

    }
}
