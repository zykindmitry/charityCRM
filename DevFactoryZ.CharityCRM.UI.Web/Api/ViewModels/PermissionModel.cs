using DevFactoryZ.CharityCRM.Services;

namespace DevFactoryZ.CharityCRM.UI.Web.Api.ViewModels
{
    public class PermissionModel
    {
        public PermissionModel()
        {
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public PermissionData ToDto()
        {
            return new PermissionData
            {
                Name = Name,
                Description = Description
            };
        }
    }
}
