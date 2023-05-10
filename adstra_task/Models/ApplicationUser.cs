using Microsoft.AspNetCore.Identity;

namespace adstra_task.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? City { get; set; }
    }
}
