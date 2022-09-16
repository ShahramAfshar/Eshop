using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace EndPoint.Site.Utilities
{
    public static class ClaimUtility
    {
        public static int? GetUserId(ClaimsPrincipal User)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;

                if (claimsIdentity.FindFirst(ClaimTypes.NameIdentifier) != null)
                {
                    int userId = int.Parse(claimsIdentity.FindFirst(ClaimTypes.NameIdentifier).Value);
                    return userId;
                }
                else
                {
                    return null;
                }

            }
            catch (Exception)
            {

                return null;
            }

        }


        public static string GetEmail(ClaimsPrincipal User)
        {
            try
            {
                var claimsIdentity = User.Identity as ClaimsIdentity;

                return claimsIdentity.FindFirst(ClaimTypes.Email).Value;
                
      

            }
            catch (Exception)
            {

                return null;
            }

        }

        public static List<string> GetRoles(ClaimsPrincipal user)
        {

            try
            {
                var claimIdentity = user.Identity as ClaimsIdentity;
                List<string> roles = new List<string>();

                foreach (var item in claimIdentity.Claims.Where(p=>p.Type.EndsWith("role")))
                {
                    roles.Add(item.Value);
                }

                return roles;

            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
