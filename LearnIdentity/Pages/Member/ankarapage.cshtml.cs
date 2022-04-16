using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace LearnIdentity.Pages.Member
{
    [Authorize(Policy = "AnkaraPolicy")]
    public class ankarapageModel : PageModel
    {      
        public void OnGet()
        {
        }
    }
}
