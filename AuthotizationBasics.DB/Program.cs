using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AuthotizationBasics.DB.Entities;
using AuthotizationBasics.Identity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace AuthotizationBasics.Roles
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

        internal static class Databaseinitializer
        {
            internal static void Init(IServiceProvider serviceProvider)
            {
                var context = serviceProvider.GetService<ApplicationDbContext>();


                var user = new ApplicationUser
                {
                    FirstName = "fedor",
                    Id = Guid.NewGuid(),
                    LastName = "sokolov",
                    Password = "qwe"
                };
                context.Users.Add(user);
                context.SaveChanges();
            }
        }
    }
}
