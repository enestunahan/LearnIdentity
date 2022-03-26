using LearnIdentity.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearnIdentity.Validations
{
    public class CustomUserNameValidator : IUserValidator<AppUser>
    {
        public Task<IdentityResult> ValidateAsync(UserManager<AppUser> manager, AppUser user)
        {
            var digits = new string[] { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

            List<IdentityError> errors = new List<IdentityError>();

            foreach(var item in digits)
            {
                if(user.UserName[0].ToString() == item)
                {
                    errors.Add(new IdentityError()
                    {
                        Code = "UserNameContainsFirstLetterDigitContains",
                        Description = "Kullanıcı adı alanı ilk harfi sayısal bir değer olamaz"
                    });
                }
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
