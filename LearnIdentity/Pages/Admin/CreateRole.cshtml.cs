using LearnIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace LearnIdentity.Pages.Admin
{
    public class CreateRoleModel : BasePageModelModel
    {
        public RoleViewModel Model { get; set; }
        public CreateRoleModel(RoleManager<AppRole> roleManager) : base(null, roleManager,null)
        {

        }
        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync(RoleViewModel model) 
        {
            AppRole role = new AppRole();
            role.Name = model.Name;
            IdentityResult result = await _roleManager.CreateAsync(role);

            if (result.Succeeded)

            {
                return Redirect("Roles");
            }
            else
            {
                AddModelError(result);
            }

            return Page();
        }
    }
}
