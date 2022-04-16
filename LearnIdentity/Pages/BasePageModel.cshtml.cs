using LearnIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LearnIdentity.Pages
{
    public class BasePageModelModel : PageModel
    {
        protected UserManager<AppUser> _userManager;
        protected SignInManager<AppUser> _signInManager;    
        protected RoleManager<AppRole> _roleManager;
        protected AppUser CurrentUser => _userManager.FindByNameAsync(User.Identity.Name).Result;
        //   User arka tarafta ClaimsPrincipal sýnýfýdýr
        public BasePageModelModel(UserManager<AppUser> userManager = null,
            RoleManager<AppRole> roleManager = null,  
            SignInManager<AppUser> signInManager =null)
        {
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userManager = userManager;
        }

        public void AddModelError(IdentityResult result)
        {
            foreach(var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
      
    }
}
