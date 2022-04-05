using LearnIdentity.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace LearnIdentity.Pages.Admin
{
    public class UpdateRoleModel : BasePageModelModel
    {
        public RoleViewModel Model{ get; set; }

        public UpdateRoleModel(RoleManager<AppRole> roleManager) : base(null,roleManager,null)
        {}
        public async Task<IActionResult> OnGetAsync(string id)
        {
            AppRole role =  await _roleManager.FindByIdAsync(id);
            Model = role.Adapt<RoleViewModel>();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(RoleViewModel model)
        {
            AppRole role = await _roleManager.FindByIdAsync(model.Id);

            if(role != null)
            {
                role.Name = model.Name;
                IdentityResult result = await _roleManager.UpdateAsync(role);

                if (result.Succeeded)
                {

                }
                else
                {
                    AddModelError(result);
                }

            }
            else
            {
                ModelState.AddModelError(string.Empty, "Güncelleme iþlemi baþarýsýz oldu");
            }
            return Redirect("/Admin/Roles");
        }
    }
}
