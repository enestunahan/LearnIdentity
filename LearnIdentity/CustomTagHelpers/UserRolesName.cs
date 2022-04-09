using LearnIdentity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LearnIdentity.CustomTagHelpers
{
    [HtmlTargetElement("td", Attributes = "user-roles")]
    public class UserRolesName : TagHelper
    {
        public UserManager<AppUser> _userManager;
        public UserRolesName(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }

        [HtmlAttributeName("user-roles")]
        public string UserId { get; set; }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            AppUser user = await _userManager.FindByIdAsync(UserId);

            IList<string> roles =  await _userManager.GetRolesAsync(user);

            string html = string.Empty;

            roles.ToList().ForEach(role =>
            {
                html += $"<span class='badge badge-info'>  {role}  </span>";
            });

            output.Content.SetHtmlContent(html);
        }
    }
}
