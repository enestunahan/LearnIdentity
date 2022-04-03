using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LearnIdentity.Pages.Member
{
    public class AccessDeniedModel : BasePageModelModel
    {
        public AccessDeniedModel() : base(null,null,null)
        {}
        public void OnGet()
        {
        }
    }
}
