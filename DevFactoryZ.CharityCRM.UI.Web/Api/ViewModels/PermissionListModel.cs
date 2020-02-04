namespace DevFactoryZ.CharityCRM.UI.Web.Api.ViewModels
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
