using AuthotizationBasics.Identity.Entities;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;

namespace AuthotizationBasics.Identity.Data
{
    public class ApplicationDbContext: IdentityDbContext<ApplicationUser, ApplicationRole,Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }        
    }
}
