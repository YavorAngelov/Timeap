using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Timeap.Web.Utilities;
using Timeap.Common.Validation;
using Timeap.Models;
using Timeap.Web.Data;

namespace Timeap.Web.Areas.Clients.Pages.Home
{
    [Authorize(Roles = WebConstants.ClientRole)]
    public class AddModel : PageModel
    {
        private readonly TimeapContext context;
        private readonly UserManager<User> userManager;

        public AddModel(
            TimeapContext context,
            UserManager<User> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

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

            this.StartDate = DateTime.UtcNow;
            this.EndDate = DateTime.UtcNow;
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var project = await this.AddProjectAsync();
            await this.AddTeamForProjectAsync(project);

            return RedirectToPage("/Projects/Details", new { id = project.Id });
        }

        private async Task<Project> AddProjectAsync()
        {
            var user = await this.userManager.GetUserAsync(this.User);

            var project = new Project()
            {
                Title = this.Title,
                Description = this.Description,
                StartDate = this.StartDate,
                EndDate = this.EndDate,
                ClientId = user.Id,
            };

            await this.context.Projects.AddAsync(project);
            await this.context.SaveChangesAsync();

            return project;
        }

        private async Task AddTeamForProjectAsync(Project project)
        {
            var team = new Team()
            {
                Name = project.Title + " Team",
                ProjectId = project.Id,
            };

            await this.context.Teams.AddAsync(team);
            await this.context.SaveChangesAsync();

            var dbProject = this.context.Projects.FirstOrDefault(p => p.Id == project.Id);
            dbProject.TeamId = team.Id;
            await this.context.SaveChangesAsync();
        }
    }
}