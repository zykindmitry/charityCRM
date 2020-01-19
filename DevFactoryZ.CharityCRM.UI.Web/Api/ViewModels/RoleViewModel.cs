using DevFactoryZ.CharityCRM.Services;
using System.Collections.Generic;
using static DevFactoryZ.CharityCRM.Role;
using System.Linq;

namespace DevFactoryZ.CharityCRM.UI.Web.Api.ViewModels
{
    public class RoleViewModel
    {
        public RoleViewModel()
        {
        }

        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<RolePermission> RolePermissions { get; set; }

        public RoleData ToDto()
        {
            return new RoleData
            {
                Name = Name,
                Description = Description,
                Permissions = RolePermissions.Select(rolePermission => 
                    new Permission(rolePermission.Permission.Name, rolePermission.Permission.Description))
            };
        }
    }
}
