using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevFactoryZ.CharityCRM.UI.Web.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class PermssionAttribute : AuthorizeAttribute, IAuthorizationFilter
    {
        public string Permissions { get; set; }

        public void OnAuthorization(AuthorizationFilterContext context)
        {
            if (string.IsNullOrEmpty(Permissions))
            {
                context.Result = new UnauthorizedResult();
                return;
            }

            var userName = context.HttpContext.User.Identity.Name;
            IEnumerable<Permission> userPermissions = null; //
            var requiredPermissions = Permissions.Split(",");

            foreach (var permission in requiredPermissions)
            {
                if (userPermissions.Select(p => p.Name).ToList().Contains(permission))
                {
                    return;
                }
            }

            context.Result = new UnauthorizedResult();
            return;
        }
    }
}