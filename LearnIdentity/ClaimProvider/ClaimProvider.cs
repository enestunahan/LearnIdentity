using LearnIdentity.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace LearnIdentity.ClaimProvider
{
    public class ClaimProvider : IClaimsTransformation
    {

        public UserManager<AppUser> _userManager;
        public ClaimProvider(UserManager<AppUser> userManager)
        {
            _userManager = userManager; 
        }
        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            if(principal != null && principal.Identity.IsAuthenticated)
            {
           
                ClaimsIdentity identity = principal.Identity as ClaimsIdentity;

                AppUser user = await _userManager.FindByNameAsync(identity.Name);

                if(user != null)
                {
                    if(user.BirthDay != null)
                    {
                        var today = DateTime.Today;
                        var age = today.Year - user.BirthDay?.Year;

                        if(age > 15)
                        {
                            Claim violenceClaim = new Claim("age", true.ToString(), ClaimValueTypes.String, "enes");
                            identity.AddClaim(violenceClaim);
                        }
                    }

                    if(user.City != null)
                    {
                        if(!principal.HasClaim(x=>x.Type == "City"))
                        {
                            Claim cityClaim  = new Claim("city",user.City,ClaimValueTypes.String,"enes");                          
                            identity.AddClaim(cityClaim);
                        }
                    }
                }
            }
            return principal;
        }
    }
}
