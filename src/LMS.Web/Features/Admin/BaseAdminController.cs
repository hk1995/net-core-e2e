using LMS.Web.Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Security.Claims;

namespace LMS.Web.Features.Admin
{
    //[Authorize]
    [Route("api/admin/[controller]")]
    public class BaseAdminController : Controller
    {
        private long _userId;

        protected long UserId
        {
            get
            {
                var claim = GetClaim(AdminClaimType.UserId);
                if (claim == null)
                    return 0;

                long.TryParse(claim.Value, out _userId);
                return _userId;
            }
        }

        public string Auth0UserId
        {
            get
            {
                var claim = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier);
                return claim == null ? string.Empty : claim.Value;
            }
        }

        private Claim GetClaim(AdminClaimType claim)
        {
            return User.Claims.FirstOrDefault(x => x.Type == claim.DisplayName);
        }
    }
}
