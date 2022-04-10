using LearnIdentity.Models;
using Mapster;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading.Tasks;

namespace LearnIdentity.Pages.Member
{
    [Authorize]
    public class IndexModel : BasePageModelModel
    {
        public UserViewModel Model { get; set; }
        public IndexModel(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) :  base(userManager,null,signInManager)
        {}

        public  IActionResult OnGet()
        {
            AppUser user = CurrentUser;
            Model = user.Adapt<UserViewModel>(); //Mapster indir bunu kullanabilmek için
            return Page();
        }
    }
}
