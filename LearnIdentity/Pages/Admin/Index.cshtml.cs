using LearnIdentity.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LearnIdentity.Pages.Admin
{
    [Authorize(Roles ="admin")]
    //[Authorize(Roles ="admin,y�netici")] birden fazla da rol ekleyebiliriz bu �ekilde
    public class IndexModel : BasePageModelModel
    {
        public IndexModel() : base(null,null,null)
        {}
        public void OnGet()
        {}
    }
}
