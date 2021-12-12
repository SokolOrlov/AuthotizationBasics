using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AuthotizationBasics.Roles
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
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
