using System.ComponentModel.DataAnnotations;

namespace adstra_task.ViewModels
{
    public class CreateRoleViewModel
    {


        [Display(Name ="Role Name")]
        [Required(ErrorMessage ="Please Enter Valid Name Admin or User")]
        public string RoleName { get; set; }
    }
}
