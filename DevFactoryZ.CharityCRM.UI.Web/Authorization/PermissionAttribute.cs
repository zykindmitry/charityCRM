using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DevFactoryZ.CharityCRM.UI.Web.Authorization
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true)]
    public class PermissionAttribute : AuthorizeAttribute
    {
        public string Name { get; }

        public PermissionAttribute(string name) : base("Permission")
        {
            Name = name;
        }
    }
}
