using AuthotizationBasics.Identity.Data;
using AuthotizationBasics.Identity.Entities;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;

namespace AuthotizationBasics.Identity
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services
                .AddDbContext<ApplicationDbContext>(options =>
                    {
                        options.UseInMemoryDatabase("db1");
                    })
                .AddIdentity<ApplicationUser, ApplicationRole>(config=> {
                    config.Password.RequiredLength = 1;
                    config.Password.RequireNonAlphanumeric = false;
                    config.Password.RequireUppercase = false;
                    config.Password.RequireDigit = false;
                })
                .AddEntityFrameworkStores<ApplicationDbContext>();

            //services.AddAuthentication("Cookie")
            //.AddCookie("Cookie", options =>
            //{
            //    options.LoginPath = "/admin/login";

            //});

            services.ConfigureApplicationCookie(config => {
                config.LoginPath = "/admin/login";
                config.AccessDeniedPath = "/admin/login";
            });

            services.AddAuthorization(options =>
            {
                options.AddPolicy("admin", builder =>
                {
                    builder.RequireClaim(ClaimTypes.Role, "administrator");
                });
            });
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });
        }
    }
}
