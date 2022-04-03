using LearnIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace LearnIdentity.Pages.MemberTransactions
{
    public class RegisterModel : BasePageModelModel
    {
        public UserRegisterModel Model { get; set; }
        public RegisterModel(UserManager<AppUser> userManager) : base(userManager,null,null)
        {}
        public void OnGet()
        {
            TempData["result"] = "false";
        }

        public async Task<IActionResult> OnPostAsync(UserRegisterModel model)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var user = new AppUser
            {
                UserName = model.UserName,
                Email = model.Email,
                PhoneNumber = model.PhoneNumber
            };
            IdentityResult result  = await _userManager.CreateAsync(user, model.Password);
            if (result.Succeeded)
            {
                TempData["result"] = "true";
                return Page();
            }
            else
            {
                AddModelError(result);
                TempData["result"] = false;
                return Page();
            }
        }

        public class UserRegisterModel
        {
            [Display(Name ="Kullanýcý Adý")]
            [Required(ErrorMessage ="Kullanýcý Adý Alaný Boþ Geçilemez")]
            public string UserName { get; set;}
            [Display(Name ="Telefon Numarasý")]
            public string PhoneNumber { get; set; }
            [Required(ErrorMessage  = "Email Alaný Gereklidir")]
            [Display(Name ="Email")]
            [EmailAddress(ErrorMessage = "Email adresiniz geçerli formatta olmalýdýr")]
            public string Email { get; set; }
            [Required(ErrorMessage ="Þifre alaný gereklidir")]
            [Display(Name ="Þifre")]
            [DataType(DataType.Password)]   
            public string Password { get; set; }
        }   
    }
}
