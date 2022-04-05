using LearnIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearnIdentity.Pages.Admin
{
    public class RolesModel : BasePageModelModel
    {
        public List<AppRole> Roles { get; set; }

        public RolesModel(RoleManager<AppRole> roleManager) : base(null, roleManager, null)
        { }     
        public async Task<IActionResult> OnGet()
        {
            await FillRole();
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(string id)
        {
            AppRole role = await _roleManager.FindByIdAsync(id);
            if(role != null)
            {
               IdentityResult result =  await _roleManager.DeleteAsync(role);
            }
            await FillRole();
            return Page();
        }
        public async Task FillRole()
        {
            Roles = await _roleManager.Roles.ToListAsync();
        }
    }
}
