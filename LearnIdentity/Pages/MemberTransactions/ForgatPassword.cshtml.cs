using LearnIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System.Web;

namespace LearnIdentity.Pages.MemberTransactions
{
    public class ForgatPasswordModel : PageModel
    {
        public ForgatPasswordViewModel Model { get; set; }
        private readonly UserManager<AppUser> _userManager;
        private readonly IConfiguration _configuration;
        public ForgatPasswordModel(UserManager<AppUser> userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }
        public void OnGet()
        {
            TempData["result"] = "failed";
        }
        public async Task<IActionResult> OnPostAsync(ForgatPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByEmailAsync(model.Email);
                if(user != null)
                {
                    string passwordResetToken  = await _userManager.GeneratePasswordResetTokenAsync(user);

                    string port = _configuration.GetValue<string>("iisExpress:sslPort");

                    string passwordResetLink = $"https://localhost:{port}/MemberTransactions/ResetPassword?userId={user.Id}&token={HttpUtility.UrlEncode(passwordResetToken)}";

                    Helper.PasswordReset.PasswordResetSendEmail(passwordResetLink, user.Email);

                    TempData["result"] = "success";
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Sistemde kayýtlý email adresi bilgisi bulunamamýþtýr");
                }
            }
            return Page();
        }

        public class ForgatPasswordViewModel
        {
            [Display(Name = "Email Adresiniz")]
            [EmailAddress]
            [Required(ErrorMessage ="Email Adresi Boþ olamaz")]
            public string Email { get; set; }
        }
    }
}
