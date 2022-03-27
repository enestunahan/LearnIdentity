using LearnIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace LearnIdentity.Pages.MemberTransactions
{
    public class ResetPasswordModel : PageModel
    {
        public ResetPasswordViewModel Model { get; set; }
        private readonly UserManager<AppUser> _userManager;

        public ResetPasswordModel(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public void OnGet(string userId , string token)
        {
            TempData["result"] = "failed";
            TempData["userId"] = userId;
            TempData["token"]  = token;
        }

        public async Task<IActionResult> OnPostAsync(ResetPasswordViewModel model)
        {
            string token = TempData["token"].ToString();
            string userId = TempData["userId"].ToString();

            AppUser user = await _userManager.FindByIdAsync(userId);

            if(user != null)
            {
                IdentityResult result = await _userManager.ResetPasswordAsync(user, token, model.Password);
                if (result.Succeeded)
                {
                    await _userManager.UpdateSecurityStampAsync(user);
                    TempData["result"] = "success";
                }
                else
                {
                    foreach(IdentityError error in result.Errors)
                    {
                        ModelState.AddModelError(string.Empty, error.Description);
                    }
                }
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Hata meydana gelmiþtir. Lütfen daha sonra tekrar deneyiniz");
            }
            return Page();
        }

        public class ResetPasswordViewModel
        {
            [Display(Name = "Yeni Þifre")]
            [DataType(DataType.Password)]
            [Required(ErrorMessage = "Þifreniz Gereklidir")]
            public string Password { get; set; }
        }
    }
}
