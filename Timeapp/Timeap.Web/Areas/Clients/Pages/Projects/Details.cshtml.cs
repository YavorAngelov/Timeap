using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Timeap.Web.Utilities;
using Timeap.Common.Validation;
using Timeap.Models;
using Timeap.Web.Data;

namespace Timeap.Web.Areas.Clients.Pages.Projects
{
    [Authorize(Roles = WebConstants.ClientRole)]
    public class DetailsModel : PageModel
    {
        private readonly TimeapContext context;
        private readonly UserManager<User> userManager;

        public DetailsModel(
            TimeapContext context,
            UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public int Id { get; set; }

        [BindProperty]
        [Required]
        [StringLength(
            ValidationConstants.TitleMaxLength,
            MinimumLength = ValidationConstants.TitleMinLength)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [BindProperty]
        [Required]
        [StringLength(
            ValidationConstants.DescritionMaxLength,
            MinimumLength = ValidationConstants.DescritionMinLength)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [BindProperty]
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "Start time")]
        public DateTime StartDate { get; set; }

        [BindProperty]
        [Required]
        [DataType(DataType.Date)]
        [Display(Name = "End time")]
        public DateTime EndDate { get; set; }

        public string Client { get; set; }

        public string Team { get; set; }

        public string ReturnUrl { get; set; }

        public IActionResult OnGet(int id)
        {

            var project = this.context.Projects
                .Include(p => p.Client)
                .Include(p => p.Team)
                .FirstOrDefault(p => p.Id == id);

            if (project == null)
            {
                return this.NotFound();
            }

            this.Id = project.Id;
            this.Title = project.Title;
            this.Description = project.Description;
            this.StartDate = project.StartDate;
            this.EndDate = project.EndDate;
            this.Client = project.Client.UserName;
            this.Team = project.Team.Name;

            return this.Page();
        }

        public async Task<IActionResult> OnPostEditAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var project = await this.context.Projects.FirstOrDefaultAsync(p => p.Id == id);

            project.Title = this.Title;
            project.Description = this.Description;
            project.StartDate = this.StartDate;
            project.EndDate = this.EndDate;

            await this.context.SaveChangesAsync();

            return RedirectToPage("/Projects/Details", new { id = project.Id });
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var project = await this.context.Projects.FirstOrDefaultAsync(p => p.Id == id);

            this.context.Projects.Remove(project);
            await this.context.SaveChangesAsync();

            return RedirectToPage("/Home/Index");
        }
    }
}