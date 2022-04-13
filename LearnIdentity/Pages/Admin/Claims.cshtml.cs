using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace LearnIdentity.Pages.Admin
{
    public class ClaimsModel : PageModel
    {
        public List<Claim> claims;
        public void OnGet()
        {
            claims = User.Claims.ToList();          
        }
    }
}
