using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Timeap.Models;
using Timeap.Web.Utilities;

namespace Timeap.Web.Areas.Developers.Controllers
{
    [Area(WebConstants.DeveloperArea)]
    [Authorize(Roles = WebConstants.DeveloperRole)]
    public class DevelopersController : Controller
    {
        public DevelopersController(UserManager<User> userManager)
        {
            this.UserManager = userManager;
        }

        protected UserManager<User> UserManager { get; private set; }
    }
}