using LearnIdentity.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearnIdentity.Validations
{
    public class CustomPasswordValidator : IPasswordValidator<AppUser>
    {
        public  Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user, string password)
        {
            List<IdentityError> errors = new List<IdentityError>();

            if (password.ToLower().Contains(user.UserName.ToLower()))
            {
                errors.Add(new IdentityError()
                {
                    Code = "PasswordContainsUserName",
                    Description = "Şifre alanı kullanıcı adı içeremez"
                });
            }
            if (password.ToLower().Contains(".."))
            {
                errors.Add(new IdentityError()
                {
                    Code = " ",
                    Description = "Şifre alanında art arda nokta işareti kullanılamaz"
                });
            }

            if(errors.Count == 0)
            {
                return Task.FromResult(IdentityResult.Success);
            }
            else
            {
                return Task.FromResult(IdentityResult.Failed(errors.ToArray()));
            }
        }
    }
}
