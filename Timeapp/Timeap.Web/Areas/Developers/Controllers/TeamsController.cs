using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Timeap.Models;
using Timeap.Services.Developer.Interfaces;

namespace Timeap.Web.Areas.Developers.Controllers
{
    public class TeamsController : DevelopersController
    {
        private readonly IDeveloperTeamsService developerTeamsService;

        public TeamsController(
            UserManager<User> userManager, 
            IDeveloperTeamsService developerTeamsService)
            :base(userManager)
        {
            this.developerTeamsService = developerTeamsService;
        }

        public async Task<IActionResult> Join(int teamId)
        {
            var team = this.developerTeamsService.GetTeam(teamId);

            var developer = await this.UserManager.GetUserAsync(this.User);
            string devId = developer.Id;

            await this.developerTeamsService.JoinTeamAsync(teamId, devId);

            return RedirectToAction("Details", new { id = teamId });
        }

        [HttpGet]
        public IActionResult Details(int teamId)
        {
            return View();
        }
    }
}