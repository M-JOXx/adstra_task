using System.ComponentModel.DataAnnotations;

namespace adstra_task.ViewModels
{
    public class EditUserViewModel
    {
        [Key]
        public string? Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }

        [Required]
        public string Email { get; set; }
        [Required]
        public string PhoneNumber { get; set; }
        public string? City { get; set; }
    }
}
