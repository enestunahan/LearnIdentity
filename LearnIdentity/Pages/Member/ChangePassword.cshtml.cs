using LearnIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace LearnIdentity.Pages.Member
{
    public class ChangePasswordModel : BasePageModelModel
    {
        public PasswordChangeViewModel Model { get; set; }
        public ChangePasswordModel(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) : base(userManager,null,signInManager)
        {}

        public void OnGet()
        {
            TempData["result"] = "failed";
        }

        public async Task<IActionResult> OnPostAsync(PasswordChangeViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = CurrentUser;

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
                        AddModelError(result);
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
