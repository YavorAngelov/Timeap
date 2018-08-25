using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Timeap.Web.Utilities;
using Timeap.Models;

namespace Timeap.Web.Areas.Users.Pages.Home
{
    [Authorize(Roles = WebConstants.LoggedRole)]
    public class IndexModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;

        public IndexModel(
            UserManager<User> userManager,
            SignInManager<User> signInManager)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
        }

        public string ReturnUrl { get; set; }

        [TempData]
        public string ErrorMessage { get; set; }

        public void OnGet(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl = returnUrl ?? Url.Content("~/");

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAssignClientRoleAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
            }

            await userManager.AddToRoleAsync(user, WebConstants.ClientRole);
            await signInManager.SignOutAsync();
            await signInManager.SignInAsync(user, isPersistent: false);

            return RedirectToPage("/Home/Index", new { area = "Clients", id = user.Id });
        }

        public async Task<IActionResult> OnPostAssignDeveloperRoleAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{userManager.GetUserId(User)}'.");
            }

            await userManager.AddToRoleAsync(user, WebConstants.DeveloperRole);
            await signInManager.SignOutAsync();
            await signInManager.SignInAsync(user, isPersistent: false);

            return RedirectToAction("Index", "Home", new { area = "Developers", id = user.Id});
        }
    }
}