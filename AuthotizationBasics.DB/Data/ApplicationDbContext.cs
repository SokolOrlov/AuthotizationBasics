using AuthotizationBasics.DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace AuthotizationBasics.Identity.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options):base(options)
        {

        }

        public DbSet<ApplicationUser> Users { get; set; }
    }
}
