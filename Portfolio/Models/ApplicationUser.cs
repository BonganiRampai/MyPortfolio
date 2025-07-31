using Microsoft.AspNetCore.Identity;

namespace Portfolio.Models
{
    public class ApplicationUser : IdentityUser
    {
        // ✅ Add your custom properties here
        public string FullName { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
