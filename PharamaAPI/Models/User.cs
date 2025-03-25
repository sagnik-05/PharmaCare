using Microsoft.AspNetCore.Identity;

namespace PharmaAPI.Models
{
    public class User : IdentityUser
    {
        public string Role { get; set; } // "Admin" or "Doctor"
    }
}
