using System.Security.Claims;
using System.Security.Principal;
using System.Linq;

namespace EventFinder.Extensions
{
    public static class IdentityExtension
    {
        public static string GetRole(this IIdentity identity)
        {
            var role = ((ClaimsIdentity)identity).Claims.Where(x=> x.Type == ClaimsIdentity.DefaultRoleClaimType).ToList();
            return role.Count != 0 ? role[0].Value : string.Empty;
        }
        public static string GetLogin(this IIdentity identity)
        {
            var login = ((ClaimsIdentity)identity).Claims.Where(x=> x.Type == ClaimsIdentity.DefaultNameClaimType).ToList();
            return login.Count != 0 ? login[0].Value : string.Empty;
        }
    }
}