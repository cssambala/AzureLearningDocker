using AzureLearningDocker.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AzureLearningDocker.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        // GET: Account/Register
        [AllowAnonymous]
        public IActionResult Register()
        {
            return View();
        }

        // POST: Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Assign Student role by default
                    await _userManager.AddToRoleAsync(user, "Student");

                    // Sign in the user
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    // Update last login
                    user.LastLoginDate = DateTime.Now;
                    await _userManager.UpdateAsync(user);

                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        // GET: Account/Login
        [AllowAnonymous]
        public async Task<IActionResult> Login(string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            // Get external login providers
            var externalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            return View(new LoginViewModel { ExternalLogins = externalLogins });
        }

        // POST: Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model, string? returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    var user = await _userManager.FindByEmailAsync(model.Email);
                    if (user != null)
                    {
                        user.LastLoginDate = DateTime.Now;
                        await _userManager.UpdateAsync(user);
                    }

                    return RedirectToLocal(returnUrl);
                }

                if (result.IsLockedOut)
                {
                    ModelState.AddModelError(string.Empty, "Account locked out. Please try again later.");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid email or password.");
                }
            }

            var externalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            model.ExternalLogins = externalLogins;

            return View(model);
        }

        // GET: Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]  
        public IActionResult ExternalLogin(string provider, string? returnUrl = null)
        {
            //var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });
            var redirectUrl = Url.Action(
    "ExternalLoginCallback",
    "Account",
    new { ReturnUrl = returnUrl },
    protocol: "https");
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        // GET: Account/ExternalLoginCallback
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string? returnUrl = null, string? remoteError = null)
        {
            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
                return RedirectToAction(nameof(Login));
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }

            // Sign in the user with this external login provider if the user already has a login.
            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                return RedirectToLocal(returnUrl);
            }

            // If the user does not have an account, then ask the user to create an account.
            if (result.IsLockedOut)
            {
                return RedirectToAction(nameof(Lockout));
            }

            // Store the information about the login provider for authenticating back later.
            //HttpContext.Session.SetString("LoginProvider", info.LoginProvider);

            var email = info.Principal?.FindFirstValue(ClaimTypes.Email) ?? string.Empty;
            var firstName = info.Principal?.FindFirstValue(ClaimTypes.GivenName) ?? string.Empty;
            var lastName = info.Principal?.FindFirstValue(ClaimTypes.Surname) ?? string.Empty;

            return RedirectToAction(nameof(ExternalLoginConfirmation), new
            {
                email = email,
                firstName = firstName,
                lastName = lastName,
                returnUrl = returnUrl
            });
        }

        // GET: Account/ExternalLoginConfirmation
        [HttpGet]
        [AllowAnonymous]
        public IActionResult ExternalLoginConfirmation(string? email = null, string? firstName = null, string? lastName = null, string? returnUrl = null)
        {
            var model = new ExternalLoginConfirmationViewModel
            {
                Email = email,
                FirstName = firstName,
                LastName = lastName
            };

            ViewData["ReturnUrl"] = returnUrl;
            return View(model);
        }

        // POST: Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(
            ExternalLoginConfirmationViewModel model, string? returnUrl = null)
        {
            if (ModelState.IsValid)
            {
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    ModelState.AddModelError(string.Empty, "Error loading external login information.");
                    return View(model);
                }

                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName ?? string.Empty,
                    LastName = model.LastName ?? string.Empty,
                    EmailConfirmed = true
                };

                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    // Assign Student role by default
                    await _userManager.AddToRoleAsync(user, "Student");

                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);

                        user.LastLoginDate = DateTime.Now;
                        await _userManager.UpdateAsync(user);

                        return RedirectToLocal(returnUrl);
                    }
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            ViewData["ReturnUrl"] = returnUrl;
            return View(model);
        }

        // POST: Account/Logout
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        // GET: Account/Lockout
        [AllowAnonymous]
        public IActionResult Lockout()
        {
            return View();
        }

        private IActionResult RedirectToLocal(string? returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
