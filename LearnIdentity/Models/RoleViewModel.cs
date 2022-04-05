using System.ComponentModel.DataAnnotations;
namespace LearnIdentity.Models
{
    public class RoleViewModel
    {
        [Display(Name ="Role İsmmi")]
        [Required(ErrorMessage ="Role ismi gereklidir")]
        public string Name { get; set; }
        public string Id { get; set; }
    }
}
