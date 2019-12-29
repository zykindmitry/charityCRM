namespace DevFactoryZ.CharityCRM.UI.Web.Api.Models
{
    public class PermissionListModel : PermissionModel
    {
        public PermissionListModel() : base()
        {
        }

        public PermissionListModel(Permission permission)
        {
            Id = permission.Id;
            Name = permission.Name;
            Description = permission.Description;
        }

        public int Id { get; set; }
    }
}
