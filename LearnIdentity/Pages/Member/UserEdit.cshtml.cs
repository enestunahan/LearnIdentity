using LearnIdentity.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace LearnIdentity.Pages.Member
{
    public class UserEditModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        public UserEditViewModel Model { get; set; }

        private readonly SignInManager<AppUser> _signInManager;
        public UserEditModel(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task OnGet()
        {
            TempData["result"] = "failed";
            AppUser  user = await _userManager.FindByNameAsync(User.Identity.Name);
            Model = user.Adapt<UserEditViewModel>();
        }

        public async Task<IActionResult> OnPostAsync(UserEditViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

                user.UserName = model.UserName;
                user.Email = model.Email;
                user.PhoneNumber = model.PhoneNumber;

                IdentityResult result = await _userManager.UpdateAsync(user);

                if (result.Succeeded)
                {
                    await _userManager.UpdateSecurityStampAsync(user);
                    await _signInManager.SignOutAsync();
                    await _signInManager.SignInAsync(user,true);
                    TempData["result"] = "success";
                }
                else
                {
                    foreach (var error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                    TempData["result"] = "failed";
                }   
                
            }
            return Page();
        }

        public class UserEditViewModel
        {
            [Required(ErrorMessage  = "Kullanýcý Adý Alaný Booþ Geçilemez")]
            public string UserName { get; set; }
            [Required(ErrorMessage ="Email alaný gereklidir")]
            [EmailAddress]
            public string Email { get; set; }
            public string PhoneNumber { get; set; }
        }
    }
}
