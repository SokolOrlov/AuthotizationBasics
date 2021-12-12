using Microsoft.AspNetCore.Identity;
using System;

namespace AuthotizationBasics.DB.Entities
{
    public class ApplicationUser
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
    }
}
