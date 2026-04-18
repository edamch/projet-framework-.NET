using System.ComponentModel.DataAnnotations;

namespace tp2.Models.ViewModels
{
    public class CreateRoleViewModels
    {
        [Required]
        [Display(Name = "Role")]

        public string RoleName { get; set; }
    }
}
