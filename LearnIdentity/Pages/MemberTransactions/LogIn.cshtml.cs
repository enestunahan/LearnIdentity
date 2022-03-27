using LearnIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace LearnIdentity.Pages.MemberTransactions
{
    public class LogInModel : PageModel
    {
        public LogInViewModel Model { get; set; }
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public LogInModel(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public void OnGet(string ReturnUrl)
        {
            TempData["ReturnUrl"] = ReturnUrl;
        }

        public async Task<IActionResult> OnPostAsync(LogInViewModel model)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByEmailAsync(model.Email);
                if(user != null)
                {
                    await _signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                    {                        
                        if (TempData["ReturnUrl"] != null)
                        {
                            return Redirect(TempData["ReturnUrl"].ToString());
                        }
                        return Redirect("/MemberTransactions/Index");
                    }
                    ModelState.AddModelError(string.Empty, "Ge�ersiz email adresi veya �ifre");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Ge�ersiz email adresi veya �ifre");
                }
            }
            return Page();
        }

        public class LogInViewModel
        {
            [Display(Name ="Email Adresiniz")]
            [Required(ErrorMessage ="Email Adres alan� bo� ge�ilemez")]
            [EmailAddress]
            public string Email { get; set; }

            [Display(Name ="�ifre")]
            [Required(ErrorMessage ="�ifre alan� gereklidir")]
            [DataType(DataType.Password)]
            [MinLength(4,ErrorMessage ="�ifre alan� minimum 4 karakter olmal�d�r")]
            public string Password { get; set; }    

            [Display(Name ="Beni Hat�rla")]
            public bool RememberMe { get; set; }
        }
    }
}
