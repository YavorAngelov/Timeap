using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;
using Timeap.Web.Utilities;

namespace Timeap.Web.Areas.Users.Pages.Home
{
    [Authorize(Roles = WebConstants.LoggedRole)]
    public class ContactModel : PageModel
    {
        private readonly IEmailSender emailSender;

        public ContactModel(
            IEmailSender emailSender)
        {
            this.emailSender = emailSender;
        }

        [BindProperty]
        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [BindProperty]
        [Required]
        [Display(Name = "Subject")]
        public string Subject { get; set; }

        [BindProperty]
        [Required]
        [Display(Name = "Message")]
        [MaxLength(5000)]
        public string Message { get; set; }

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

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            await emailSender.SendEmailAsync(this.Email, this.Subject, this.Message);

            //StatusMessage = "Email sent. We will contact you as soon as possible.";
            return RedirectToPage("./Index");
        }
    }
}