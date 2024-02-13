using IdentityApp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace IdentityApp.TagHelpers
{
    // td etiketi icinde calissin. attributes ozelligi ile td icindeki taghelper etiket kapsami tanimlandi
    [HtmlTargetElement("td", Attributes = "asp-role-users")]
    public class RoleUsersTagHelper : TagHelper
    {
        private readonly RoleManager<AppRole> _roleManager;
        private readonly UserManager<AppUser> _userManager;
        public RoleUsersTagHelper(RoleManager<AppRole> roleManager, UserManager<AppUser> userManager)
        {
            _roleManager = roleManager;
            _userManager = userManager;
        }

        [HtmlAttributeName("asp-role-users")]
        public string RoleId { get; set; } = null!;

        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            var userNames = new List<string>();
            var role = await _roleManager.FindByIdAsync(RoleId);

            if (role != null && role.Name != null)
            {
                // eger kullanici rola sahip mi?
                foreach (var user in _userManager.Users)
                {
                    if (await _userManager.IsInRoleAsync(user, role.Name))
                    {
                        // Eger null ise bos deger aktar
                        userNames.Add(user.UserName ?? "");
                    }
                }
                // disariya deger dondur
                output.Content.SetHtmlContent(userNames.Count == 0 ? "kullanıcı yok" : setHtml(userNames));
            }
        }

        private string setHtml(List<string> userNames)
        {
            var html = "<ul>";
            foreach (var item in userNames)
            {
                html += "<li>" + item + "</li>";
            }
            html += "</ul>";
            return html;
        }
    }
}