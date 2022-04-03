using LearnIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace LearnIdentity.Pages.MemberTransactions
{
    public class LogInModel : BasePageModelModel
    {
        public LogInViewModel Model { get; set; }

        public LogInModel(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager) : base(userManager,null,signInManager)
        {}
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
                    if(await _userManager.IsLockedOutAsync(user))
                    {
                        ModelState.AddModelError(string.Empty, "Hesab�n�z bir s�reli�ine kilitlenmi�tir , l�tfen daha sonra tekrar deneyiniz");
                        return Page();
                    }

                    await _signInManager.SignOutAsync();
                    Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
                    if (result.Succeeded)
                    {
                        await _userManager.ResetAccessFailedCountAsync(user);

                        if (TempData["ReturnUrl"] != null)
                        {
                            return Redirect(TempData["ReturnUrl"].ToString());
                        }
                        return Redirect("/Member/Index");
                    }
                    else
                    {
                        await _userManager.AccessFailedAsync(user);

                        int fail = await _userManager.GetAccessFailedCountAsync(user);

                        ModelState.AddModelError("", $"{fail} kez ba�ar�s�z giri�");

                        if(fail == 3)
                        {
                            await _userManager.SetLockoutEndDateAsync(user, new System.DateTimeOffset(DateTime.Now.AddMinutes(30)));
                            ModelState.AddModelError("", "Hesab�n�z 3 ba�ar�s�z giri�ten dolay� 20 dakika s�reyle kitlenmi�tir. L�tfen daha sonra tekrar deneyiniz.");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Email adresiniz veya �ifreniz yanl��");
                        }
                    }                  
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
