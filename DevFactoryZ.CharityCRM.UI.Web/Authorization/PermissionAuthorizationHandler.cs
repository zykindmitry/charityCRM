using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace DevFactoryZ.CharityCRM.UI.Web.Authorization
{
    public class PermissionAuthorizationHandler : AttributeAuthorizationHandler<PermissionAuthorizationRequirement, PermissionAttribute>
    {
        private Task<bool> ok;

        protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissionAuthorizationRequirement requirement, IEnumerable<PermissionAttribute> attributes)
        {
            foreach (var permissionAttribute in attributes)
            {
                if (!await AuthorizeAsync(context.User, permissionAttribute.Name))
                {
                    return;
                }
            }

            context.Succeed(requirement);
        }

        private Task<bool> AuthorizeAsync(ClaimsPrincipal user, string permission)
        {
            return ok;
        }
    }
}
