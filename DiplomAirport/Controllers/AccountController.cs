using DiplomAirport.Helpers;
using DiplomAirport.Models;
using DiplomAirport.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DiplomAirport.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly EmailService _emailService;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            EmailService emailService)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _emailService = emailService;
        }


        [HttpGet]
        public IActionResult Login() => View();

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null && !user.EmailConfirmed &&
                    (await _userManager.CheckPasswordAsync(user, model.Password)))
                {
                    ModelState.AddModelError("", "Email not confirmed yet");
                    return View(model);
                }

                var result = await _signInManager.PasswordSignInAsync
                    (model.Email, model.Password, model.RememberMe, false);

                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
                    {
                        return Redirect(returnUrl);
                    }
                    else
                    {
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Invalid Login Attempt");
                }
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        [HttpGet]
        public IActionResult Registration() => View();

        //[HttpGet]
        //public IActionResult SendMessage()
        //{
        //    _emailService.SendEmail("spn337@gmail.com", "Confirm your account",
        //                $"Confirm your registration by link");
        //    return RedirectToAction("Index", "Home");
        //}
        [HttpPost]
        public async Task<IActionResult> Registration(RegistrationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    UserName = model.Email,
                    Gender = model.Gender,
                    Country = model.Country
                };

                var roleNameDefault = "User";
                var role = await _roleManager.FindByNameAsync(roleNameDefault);
                if (role == null)
                {
                    role = new IdentityRole
                    {
                        Name = roleNameDefault
                    };
                    await _roleManager.CreateAsync(role);
                }

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, role.Name);

                    var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    var confirmationLink = Url.Action("ConfirmEmail", "Account",
                        new { userId = user.Id, token }, Request.Scheme);

                    _emailService.SendEmail(model.Email, "Confirm your account",
                        $"Confirm your registration by : <a href='{confirmationLink}'>link</a>");

                    ViewBag.Title = "Registration successful";
                    ViewBag.Message = "Before you can Login, please confirm your email";
                    return View("Info");
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError("", error.Description);
                    }
                }
            }
            return View(model);
        }


        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            if (userId == null || token == null)
            {
                ViewBag.ErrorMessage = $"The User ID {userId} or token {token} are invalid";
                return View("Error");
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                ViewBag.ErrorMessage = $"The User ID {userId} is invalid";
                return View("NotFound");
            }
            else if (user.EmailConfirmed)
            {
                ViewBag.ErrorTitle = "Email is already confirming";
                return View("Error");
            }

            var result = await _userManager.ConfirmEmailAsync(user, token);

            if (result.Succeeded)
            {
                ViewBag.Message = "Thank you to comfirm your e-mail";
                return View("Info");
            }
            else
            {
                ViewBag.ErrorTitle = "Email cannot be confirmed";
                return View("Error");
            }
        }


        [AcceptVerbs("Get", "Post")]
        public async Task<IActionResult> IsEmailInUse(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);

            if (user == null)
            {
                return Json(true);
            }
            else
            {
                return Json($"Email {email} is already in use");
            }
        }

        [HttpGet]
        public IActionResult AccessDenied() => View();

        [HttpGet]
        public IActionResult ForgotPassword() => View();

        [HttpPost]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);

                if (user != null && await _userManager.IsEmailConfirmedAsync(user))
                {
                    var token = await _userManager.GeneratePasswordResetTokenAsync(user);

                    var passwordResetLink = Url.Action("ResetPassword", "Account",
                        new { email = model.Email, token }, Request.Scheme);

                    _emailService.SendEmail(model.Email, "Reset your password",
                        $"Reset your password by : <a href='{passwordResetLink}'>link</a>");

                }

                ViewBag.Message = "We sent an email with the instructions to reset your password";
                return View("Info");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult ResetPassword(string email, string token)
        {
            if (email == null || token == null)
            {
                ModelState.AddModelError("", "Invalid password reset token");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);

                    if (!result.Succeeded)
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("", error.Description);
                        }
                        return View(model);
                    }
                }
                ViewBag.Message = "Thank you to reset your password";
                return View("Info");
            }
            return View(model);
        }
    }
}
