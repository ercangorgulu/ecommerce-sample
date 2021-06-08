using Microsoft.AspNetCore.Authorization;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Infra.CrossCutting.Identity.Authorization
{
    public class ClaimRequirement : AuthorizationHandler<ClaimRequirement>, IAuthorizationRequirement
    {
        public ClaimRequirement(string claimName, string claimValue)
        {
            ClaimName = claimName;
            ClaimValue = claimValue;
        }

        public string ClaimName { get; set; }
        public string ClaimValue { get; set; }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context,
                                                       ClaimRequirement requirement)
        {
            var claim = context.User.Claims.FirstOrDefault(x => x.Type == requirement.ClaimName);
            if (claim != null && claim.Value.Contains(requirement.ClaimValue))
            {
                context.Succeed(requirement);
            }

            return Task.CompletedTask;
        }
    }
}
