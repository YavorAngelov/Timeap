using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Timeap.Common.Models.BindingModels;
using Timeap.Models;
using Timeap.Services.Developer.Interfaces;

namespace Timeap.Web.Areas.Developers.Controllers
{
    public class SolutionsController : DevelopersController
    {
        private readonly IDeveloperSolutionsService developerSolutionsService;

        public SolutionsController(
            UserManager<User> userManager,
            IDeveloperSolutionsService developerSolutionsService) 
            : base(userManager)
        {
            this.developerSolutionsService = developerSolutionsService;
        }

        [HttpGet]
        public IActionResult Create(int teamId)
        {
            TempData["TeamId"] = teamId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SolutionCreateBindingModel model, int id)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            int teamId = int.Parse(TempData["TeamId"].ToString());
            await this.developerSolutionsService.CreateAsync(model, id);

            return RedirectToAction("Details");
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var model = await this.developerSolutionsService.GetDetailsAsync(id);
            return View(model);
        }
    }
}