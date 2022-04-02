using LearnIdentity.Models;
using Mapster;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using System;
using LearnIdentity.Enums;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IO;

namespace LearnIdentity.Pages.Member
{
    public class UserEditModel : PageModel
    {
        private readonly UserManager<AppUser> _userManager;
        public UserViewModel Model { get; set; }

        private readonly SignInManager<AppUser> _signInManager;
        public SelectList _genders { get; set; }
        public UserEditModel(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public async Task OnGet()
        {
            TempData["result"] = "failed";
            AppUser  user = await _userManager.FindByNameAsync(User.Identity.Name);
            Model = user.Adapt<UserViewModel>();
            _genders = new SelectList(Enum.GetNames(typeof(Gender)));
        }

        public async Task<IActionResult> OnPostAsync(UserViewModel model ,IFormFile userPicture)
        {
            if (ModelState.IsValid)
            {
                AppUser user = await _userManager.FindByNameAsync(User.Identity.Name);

                if (userPicture != null && userPicture.Length > 0)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(userPicture.FileName);

                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/UserPicture", fileName);

                    using (var stream = new FileStream(path, FileMode.Create))
                    {
                        await userPicture.CopyToAsync(stream);

                        user.Picture = "/UserPicture/" + fileName;
                    }
                }

                user.City = model.City;
                user.BirthDay = model.BirthDay;
                user.Gender = (int)model.Gender;
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

        //public class UserEditViewModel
        //{
        //    [Required(ErrorMessage  = "Kullanýcý Adý Alaný Booþ Geçilemez")]
        //    public string UserName { get; set; }
        //    [Required(ErrorMessage ="Email alaný gereklidir")]
        //    [EmailAddress]
        //    public string Email { get; set; }

        //    [Display(Name ="Telefon Numarasý")]
        //    public string PhoneNumber { get; set; }

        //    [Display (Name ="Doðum Tarihi")]
        //    [DataType(DataType.DateTime)]
        //    public DateTime? BirhtDay { get; set; }

        //    [Display(Name ="Profil Fotoðrafý")]
        //    public string Picture { get; set; }
        //    [Display(Name = "Þehir")]
        //    public string City { get; set; }
        //    [Display(Name ="Cinsiyet")]
        //    public Gender Gender { get; set; }
        //}
    }
}
