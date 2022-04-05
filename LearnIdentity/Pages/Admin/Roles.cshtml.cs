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
            Roles =  await _roleManager.Roles.ToListAsync();
            return Page();
        }
    }
}
