using LearnIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace LearnIdentity.Pages.Member
{
    public class ChangePasswordModel : PageModel
    {
        public PasswordChangeViewModel Model { get; set; }
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public ChangePasswordModel(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public void OnGet()
        {
            TempData["result"] = "failed";
        }

        public async Task<IActionResult> OnPostAsync(PasswordChangeViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

                bool exist = await _userManager.CheckPasswordAsync(user, model.PasswordOld);

                if (exist)
                {
                    IdentityResult result  = await _userManager.ChangePasswordAsync(user,model.PasswordOld,model.PasswordNew);
                    if (result.Succeeded)
                    {
                        await _userManager.UpdateSecurityStampAsync(user);
                        await _signInManager.SignOutAsync();
                        await _signInManager.PasswordSignInAsync(user,model.PasswordNew,true,false);
                        TempData["result"] = "success";
                    }
                    else
                    {
                        foreach(IdentityError error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty,error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Eski þifreniz yanlýþ");
                    TempData["result"] = false;
                }
            }
            
            return Page();
        }
        public class PasswordChangeViewModel
        {
            [Display(Name = "Eski þifreniz")]
            [Required(ErrorMessage = "Eski þifreniz gereklidir")]
            [DataType(DataType.Password)]
            [MinLength(4, ErrorMessage = "Þifreniz en az 4 karakterli olmak zorundadýr.")]
            public string PasswordOld { get; set; }

            [Display(Name = "Yeni þifreniz")]
            [Required(ErrorMessage = "Yeni þifreniz gereklidir")]
            [DataType(DataType.Password)]
            [MinLength(4, ErrorMessage = "Þifreniz en az 4 karakterli olmak zorundadýr.")]
            public string PasswordNew { get; set; }

            [Display(Name = "Onay yeni þifreniz")]
            [Required(ErrorMessage = "Onay yeni þifre gereklidir")]
            [DataType(DataType.Password)]
            [MinLength(4, ErrorMessage = "Þifreniz en az 4 karakterli olmak zorundadýr.")]
            [Compare("PasswordNew", ErrorMessage = "Yeni þifreniz  ve onay þifreniz birbirinden farklýdýr.")]
            public string PasswordConfirm { get; set; }
        }
    }
}
