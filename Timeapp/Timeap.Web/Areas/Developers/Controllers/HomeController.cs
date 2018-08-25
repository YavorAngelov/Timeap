using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Timeap.Common.Models.BindingModels;
using Microsoft.AspNetCore.Identity.UI.Services;
using Timeap.Services.Developer.Interfaces;
using Microsoft.AspNetCore.Identity;
using Timeap.Models;

namespace Timeap.Web.Areas.Developers.Controllers
{
    public class HomeController : DevelopersController
    {
        private readonly IEmailSender emailSender;
        private readonly IDeveloperProjectsService developerProjectsService;

        public HomeController(
            UserManager<User> userManager,
            IEmailSender emailSender,
            IDeveloperProjectsService developerProjectsService)
            : base(userManager)
        {
            this.emailSender = emailSender;
            this.developerProjectsService = developerProjectsService;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string id)
        {
            var currentUser = await this.UserManager.GetUserAsync(this.User);
            ViewData["DeveloperName"] = currentUser.UserName;

            var latestProjects = await this.developerProjectsService.GetLatestProjectsAsync();

            return View(latestProjects);
        }

        [HttpGet]
        public IActionResult Contact()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Contact(UserContactBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            await emailSender.SendEmailAsync(model.Email, model.Subject, model.Message);

            //StatusMessage = "Email sent. We will contact you as soon as possible.";
            return RedirectToAction("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }
    }
}