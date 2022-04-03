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
    public class UserEditModel : BasePageModelModel
    {
        public UserViewModel Model { get; set; }
        public SelectList _genders { get; set; }
        public UserEditModel(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager):  base(userManager,null,signInManager)
        {}
        public void OnGet()
        {
            TempData["result"] = "failed";
            AppUser user = CurrentUser;
            Model = user.Adapt<UserViewModel>();
            _genders = new SelectList(Enum.GetNames(typeof(Gender)));
        }

        public async Task<IActionResult> OnPostAsync(UserViewModel model ,IFormFile userPicture)
        {
            if (ModelState.IsValid)
            {
                AppUser user = CurrentUser;

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
                    AddModelError(result);
                    TempData["result"] = "failed";
                }   
                
            }
            return Page();
        }
    }
}
