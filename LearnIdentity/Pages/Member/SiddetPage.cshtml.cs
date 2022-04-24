using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LearnIdentity.Pages.Member
{
    [Authorize(Policy ="ViolencePolicy")]
    public class SiddetPageModel : PageModel
    {
        public void OnGet()
        {
        }
    }
}
