using System.ComponentModel.DataAnnotations;

namespace adstra_task.ViewModels
{
    public class RegisterViewModel
    {

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
        [Compare("Password", ErrorMessage = "Sorry,New password and confirm password don't match")]
        public string ConfirmPassword { get; set; }


        [Required]
        public string PhoneNumber { get; set; }

        public string? City { get; set; }
    }
}
