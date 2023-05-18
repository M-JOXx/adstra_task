using System.ComponentModel.DataAnnotations;

namespace adstra_task.ViewModels
{
    public enum Roles
    {
        Admin,User
    }
    public class CreateRoleViewModel
    {


        [Display(Name ="Role Name")]
        [Required(ErrorMessage ="Please Enter Valid Name Admin or User")]
        public Roles RoleName { get; set; }
    }
}
