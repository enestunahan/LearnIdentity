using LearnIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LearnIdentity.Pages.Member
{
    public class LogOutModel : BasePageModelModel
    {
        public LogOutModel(SignInManager<AppUser>  signInManager): base(null,null,signInManager)
        {}
        public void OnGet()
        {
            _signInManager.SignOutAsync();
        }
    }
}
