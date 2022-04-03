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
                        ModelState.AddModelError(string.Empty, "Hesabýnýz bir süreliðine kilitlenmiþtir , lütfen daha sonra tekrar deneyiniz");
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

                        ModelState.AddModelError("", $"{fail} kez baþarýsýz giriþ");

                        if(fail == 3)
                        {
                            await _userManager.SetLockoutEndDateAsync(user, new System.DateTimeOffset(DateTime.Now.AddMinutes(30)));
                            ModelState.AddModelError("", "Hesabýnýz 3 baþarýsýz giriþten dolayý 20 dakika süreyle kitlenmiþtir. Lütfen daha sonra tekrar deneyiniz.");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Email adresiniz veya þifreniz yanlýþ");
                        }
                    }                  
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Geçersiz email adresi veya þifre");
                }
            }
            return Page();
        }

        public class LogInViewModel
        {
            [Display(Name ="Email Adresiniz")]
            [Required(ErrorMessage ="Email Adres alaný boþ geçilemez")]
            [EmailAddress]
            public string Email { get; set; }

            [Display(Name ="Þifre")]
            [Required(ErrorMessage ="Þifre alaný gereklidir")]
            [DataType(DataType.Password)]
            [MinLength(4,ErrorMessage ="Þifre alaný minimum 4 karakter olmalýdýr")]
            public string Password { get; set; }    

            [Display(Name ="Beni Hatýrla")]
            public bool RememberMe { get; set; }
        }
    }
}
