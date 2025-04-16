using Microsoft.AspNetCore.Identity;

namespace POS.Api.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
    }
}
