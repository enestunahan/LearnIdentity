using LearnIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LearnIdentity.Pages.Member
{
    public class LogOutModel : PageModel
    {
        public SignInManager<AppUser> _signInManager;
        public LogOutModel(SignInManager<AppUser>  signInManager)
        {
            _signInManager = signInManager;
        }
        public void OnGet()
        {
            _signInManager.SignOutAsync();
        }
    }
}
