using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Timeap.Models;
using Timeap.Web.Utilities;

namespace Timeap.Web.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class LoginModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly ILogger<LoginModel> logger;

        public LoginModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager, 
            ILogger<LoginModel> logger)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.logger = logger;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public class InputModel
        {
            [Required]
            public string Username { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Display(Name = "Remember me?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var result = await signInManager.PasswordSignInAsync(Input.Username, Input.Password, Input.RememberMe, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    var user = await this.userManager.FindByNameAsync(Input.Username);
                    var roles = await this.userManager.GetRolesAsync(user);

                    if (!roles.Contains(WebConstants.LoggedRole))
                    {
                        await userManager.AddToRoleAsync(user, WebConstants.LoggedRole);

                        await signInManager.SignInAsync(user, isPersistent: false);
                        await signInManager.SignOutAsync();
                        await signInManager.SignInAsync(user, isPersistent: false);
                    }

                    if (roles.Contains(WebConstants.ClientRole))
                    {
                        return RedirectToPage("/Home/Index", new { area = "Clients", id = user.Id });
                    }
                    else if (roles.Contains(WebConstants.DeveloperRole))
                    {
                        return RedirectToAction("Index", "Home", new { area = "Developers" });
                    }

                    logger.LogInformation("User logged in.");
                    return RedirectToPage("/Home/Index", new { area = "Users" });
                    //return RedirectToAction("Index", "Home", new { area = "Users" });
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    logger.LogWarning("User account locked out.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
