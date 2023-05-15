using System.ComponentModel.DataAnnotations;

namespace adstra_task.ViewModels
{
    public class LoginViewModel
    {

        [Key]
        [Required]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Password"), Required(ErrorMessage = "The Password field is required")]
        public string Password { get; set; }

        public bool RememberMe { get; set; }



    }
}
