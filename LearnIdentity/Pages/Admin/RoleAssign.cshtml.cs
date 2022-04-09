using LearnIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnIdentity.Pages.Admin
{
    public class RoleAssignModel : BasePageModelModel
    {
        public List<RoleAssignViewModel> Model { get; set; }
        public RoleAssignModel(UserManager<AppUser> userManager , RoleManager<AppRole> roleManager) : base(userManager,roleManager)
        {}
        public async Task<IActionResult> OnGetAsync(string id)
        {
            TempData["userId"] = id;
         
            AppUser user = await _userManager.FindByIdAsync(id);

            TempData["userName"] = user.UserName;

            List<AppRole> roles = await _roleManager.Roles.ToListAsync();
            List<string> userRoles = await _userManager.GetRolesAsync(user) as List<string>;
            Model = new List<RoleAssignViewModel>();

            foreach (var role in roles)
            {
                RoleAssignViewModel r = new RoleAssignViewModel();
                r.RoleId = role.Id;
                r.RoleName = role.Name;
                if (userRoles.Contains(role.Name))
                {
                    r.Exist = true;
                }
                else
                {
                    r.Exist = false;
                }
                Model.Add(r);
            }

            return Page();
        }


        public async Task<IActionResult> OnPostAsync(List<RoleAssignViewModel> model)
        {
            AppUser user = await _userManager.FindByIdAsync(TempData["userId"].ToString());

            foreach(var role in model)
            {
                if (role.Exist)
                {
                    await _userManager.AddToRoleAsync(user, role.RoleName);
                }
                else
                {
                    await _userManager.RemoveFromRoleAsync(user, role.RoleName);
                }
            }
            
            return Redirect("/Admin/Users");
        }

        public class RoleAssignViewModel
        {
            public string RoleId { get; set; }
            public string RoleName { get; set; }
            public bool Exist { get; set; }
        }
    }
}
