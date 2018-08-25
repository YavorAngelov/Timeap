using Microsoft.AspNetCore.Hosting;

[assembly: HostingStartup(typeof(Timeap.Web.Areas.Identity.IdentityHostingStartup))]
namespace Timeap.Web.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
            });
        }
    }
}