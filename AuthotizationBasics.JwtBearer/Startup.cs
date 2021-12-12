using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace AuthotizationBasics.JwtBearer
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            byte[] secretBytes = Encoding.UTF8.GetBytes(Constants.SecretKey);
            var key = new SymmetricSecurityKey(secretBytes);

            services.AddControllersWithViews();
            services.AddAuthentication("OAuth")
               .AddJwtBearer("OAuth", config=> {
                   config.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidIssuer = Constants.Issuer,
                       ValidAudience = Constants.Audience,
                       IssuerSigningKey = key
                   };
               });
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
