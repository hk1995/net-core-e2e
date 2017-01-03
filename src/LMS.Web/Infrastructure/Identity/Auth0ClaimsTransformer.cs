using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;

namespace LMS.Web.Infrastructure.Identity
{
    //http://benfoster.io/blog/customising-claims-transformation-in-aspnet-core-identity
    public class Auth0ClaimsTransformer : IClaimsTransformer
    {

        private string _userId = AdminClaimType.UserId.DefaultValue;

        public Task<ClaimsPrincipal> TransformAsync(ClaimsTransformationContext context)
        {
            //TODO: Clean up and simplify AdminClaimTypes Transformer
            foreach (var claim in context.Principal.Claims)
            {
                switch (claim.Type)
                {
                    case "userId":
                        _userId = claim.Value ?? _userId;
                        break;
                }
            }
            ((ClaimsIdentity)context.Principal.Identity)
                .AddClaims(new Claim[]
                {
                    new Claim(AdminClaimType.UserId.DisplayName, _userId)
                });

            return Task.FromResult(context.Principal);
        }
    }
}
