using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PhotoSharinggApplication.Helper
{
    public static class DapatkanIdentity
    {
        public static string GetUsername(this ClaimsPrincipal user)
        {
            if (user.Identity.IsAuthenticated)
            {
                return user.Claims.FirstOrDefault(x => x.Type == "Username")?.Value ?? string.Empty;
            }

            return string.Empty;
        }

        public static string GetRole(this ClaimsPrincipal user)
        {
            if (user.Identity.IsAuthenticated)
            {
                return user.Claims.FirstOrDefault(x => x.Type == "Role")?.Value ?? string.Empty;
            }

            return string.Empty;
        }
    }
}
