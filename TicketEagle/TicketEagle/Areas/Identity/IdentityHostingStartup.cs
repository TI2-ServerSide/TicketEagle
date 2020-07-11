using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using TicketEagle.Areas.Identity.Data;
using TicketEagle.Data;

[assembly: HostingStartup(typeof(TicketEagle.Areas.Identity.IdentityHostingStartup))]
namespace TicketEagle.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<TEDbContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("TEDbContextConnection")));

                services.AddDefaultIdentity<TicketEagleUser>(options => options.SignIn.RequireConfirmedAccount = false)
                    .AddRoles<IdentityRole>()
                    .AddEntityFrameworkStores<TEDbContext>();
            });
        }
    }
}