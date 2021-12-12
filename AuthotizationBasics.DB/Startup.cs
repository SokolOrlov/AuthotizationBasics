using AuthotizationBasics.Identity.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System.Security.Claims;

namespace AuthotizationBasics.Roles
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(config=> {
                config.UseInMemoryDatabase("db1");
            });

            services.AddAuthentication("Cookie")
            .AddCookie("Cookie", options => {
                options.LoginPath = "/admin/login";
                options.AccessDeniedPath = "/admin/login";

            });
            services.AddAuthorization(options=>
            {
                options.AddPolicy("admin", builder => {
                   // builder.RequireClaim(ClaimTypes.Role, "administrator");
                    //builder.RequireAssertion(x => x.User.IsInRole("administrator") || x.User.IsInRole("manager"));
                    builder.RequireAssertion(x => x.User.HasClaim(ClaimTypes.Role, "administrator")
                    || x.User.HasClaim(ClaimTypes.Role, "manager"));
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
