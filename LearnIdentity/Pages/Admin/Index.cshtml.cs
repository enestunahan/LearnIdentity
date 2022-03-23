using LearnIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearnIdentity.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        public IEnumerable<AppUser> users; 
        public IndexModel(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task OnGet()
        {
            users = await _userManager.Users.ToListAsync();
        }
    }
}
