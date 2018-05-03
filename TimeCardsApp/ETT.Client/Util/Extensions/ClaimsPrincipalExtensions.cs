using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Threading.Tasks;

namespace ETT.Web.Util.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int? Identifier(this ClaimsPrincipal principal)
        {
            if (principal == null) throw new ArgumentNullException(nameof(principal));
            string identifier = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            return identifier == null ? default(int?) : Convert.ToInt32(identifier);
        }
    }
}
