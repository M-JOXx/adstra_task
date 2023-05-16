using System.ComponentModel.DataAnnotations;

namespace adstra_task.ViewModels
{
    public class EditUserViewModel
    {
        [Key]
        public string? Id { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? City { get; set; }
    }
}
