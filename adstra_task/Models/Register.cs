
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace adstra_task.Models
{
    public class Register
    {
        [Key]
        public string? Id { get; set; }

        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare("Password", ErrorMessage = "Sorry, The Confrirm Password Do not match" )]
        public string ConfirmPassword { get; set; }


        [Required]
        public string PhoneNumber { get; set; }

        public string? City { get; set; }

    }
}
