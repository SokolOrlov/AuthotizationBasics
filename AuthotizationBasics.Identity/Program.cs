using AuthotizationBasics.Identity.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace AuthotizationBasics.Identity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var scope = host.Services.CreateScope())
            {
                Databaseinitializer.Init(scope.ServiceProvider);
            }

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }

    internal static class Databaseinitializer
    {
        internal static void Init(IServiceProvider serviceProvider)
        {
            //var context = serviceProvider.GetService<ApplicationDbContext>();


            //var user = new ApplicationUser
            //{
            //    FirstName = "fedor",
            //    Id = Guid.NewGuid(),
            //    LastName = "sokolov", 
            //    UserName = "tapok"
            //};
            //context.Users.Add(user);
            //context.SaveChanges();

            var userManager = serviceProvider.GetService<UserManager<ApplicationUser>>();

            var user = new ApplicationUser
            {
                FirstName = "fedor", 
                LastName = "sokolov",
                UserName = "tapok"
            };

            var result  = userManager.CreateAsync(user, "qwe").GetAwaiter().GetResult();

            if (!result.Succeeded)
            {
                throw new Exception("create user faild");
            }
            else
            {
                userManager.AddClaimAsync(user, new System.Security.Claims.Claim(System.Security.Claims.ClaimTypes.Role, "administrator"));
            }
        }
    }
}
