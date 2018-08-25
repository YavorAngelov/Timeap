using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Timeap.Web.Utilities;
using Timeap.Models;
using Timeap.Web.Data;

namespace Timeap.Web.Utilities
{
    public static class ApplicationBuilderAuthExtensions
    {
        private static readonly IdentityRole[] roles =
        {
            new IdentityRole(WebConstants.ClientRole),
            new IdentityRole(WebConstants.DeveloperRole),
            new IdentityRole(WebConstants.LoggedRole)
        };

        public static async void SeedRoles(this IApplicationBuilder app)
        {
            var serviceFactory = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>();

            var scope = serviceFactory.CreateScope();
            using (scope)
            {
                var roleManager = scope.ServiceProvider.
                    GetRequiredService<RoleManager<IdentityRole>>();

                var userManager = scope.ServiceProvider
                    .GetRequiredService<UserManager<User>>();

                foreach (var role in roles)
                {
                    if (!await roleManager.RoleExistsAsync(role.Name))
                    {
                        await roleManager.CreateAsync(role);
                    }
                }

                var user = await userManager.FindByNameAsync("clientAdmin");
                if (user == null)
                {
                    user = new User()
                    {
                        UserName = "clientAdmin",
                        Email = "clientAdmin@mail.com"
                    };

                    await userManager.CreateAsync(user, "clientAdmin");
                    await userManager.AddToRoleAsync(user, roles[0].Name);
                }
            }
        }

        //public static async void SeedDatabase(this IApplicationBuilder app, TimeapContext context)
        //{
        //}
    }
}
