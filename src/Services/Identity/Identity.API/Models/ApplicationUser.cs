using System;
using Microsoft.AspNetCore.Identity;

namespace Identity.API.Models
{
    // Add profile data for application users by adding properties to the ApplicationUser class
    public class ApplicationUser : IdentityUser
    {
        public Guid? TenantId { get; set; }
        public string ExtraProperties { get; set; }
    }
}
