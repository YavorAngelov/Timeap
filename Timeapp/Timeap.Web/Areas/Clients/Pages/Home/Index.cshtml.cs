using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Timeap.Web.Utilities;
using Timeap.Models;
using Timeap.Web.Data;

namespace Timeap.Web.Areas.Clients.Pages.Home
{
    [Authorize(Roles = WebConstants.ClientRole)]
    public class IndexModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly TimeapContext context;

        public IndexModel(
            UserManager<User> userManager,
            TimeapContext context)
        {
            this.userManager = userManager;
            this.context = context;
        }

        public string ClientName { get; set; }

        public List<Project> ClientProjects { get; set; }

        public async Task OnGetAsync(string id)
        {
            var client = await this.userManager.GetUserAsync(this.User);
            this.ClientName = client.UserName;

            this.GetLatestProjects(client);
        }

        private void GetLatestProjects(User client)
        {
            this.ClientProjects = this.context.Projects
                .Where(p => p.ClientId == client.Id)
                .Include(p => p.Team)
                .OrderByDescending(p => p.StartDate)
                .Take(6)
                .ToList();
        }
    }
}