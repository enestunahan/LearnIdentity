using LearnIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearnIdentity.Pages.Admin
{
    public class UsersModel : BasePageModelModel
    {
        public IEnumerable<AppUser> _users;
        public UsersModel(UserManager<AppUser> userManager) : base(userManager)
        {
                
        }
        public async Task OnGet()
        {
            _users = await _userManager.Users.ToListAsync();
        }
    }
}
