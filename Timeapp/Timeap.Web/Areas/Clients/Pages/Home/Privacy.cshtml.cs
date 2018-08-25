using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Timeap.Web.Utilities;

namespace Timeap.Web.Areas.Clients.Pages.Home
{
    [Authorize(Roles = WebConstants.LoggedRole)]
    public class PrivacyModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}