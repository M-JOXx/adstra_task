using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace adstra_task.Models
{
    public class ApplicationUser : IdentityUser
    {
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }
        public string? City { get; set; }
    }
}
