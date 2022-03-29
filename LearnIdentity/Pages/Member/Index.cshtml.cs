using LearnIdentity.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace LearnIdentity.Pages.Member
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        public UserViewModel Model { get; set; }
        public IndexModel(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<IActionResult> OnGet()
        {
            AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            Model = user.Adapt<UserViewModel>(); //Mapster indir bunu kullanabilmek için
            return Page();
        }


        public class UserViewModel
        {
            public string Email { get; set; }
            public string UserName { get; set; }
            public string PhoneNumber { get; set; }
        }
    }
}
