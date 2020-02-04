namespace DevFactoryZ.CharityCRM.UI.Web.Api.ViewModels
{
    public class RoleListViewModel : RoleViewModel
    {
        public RoleListViewModel() : base()
        {
        }

        public RoleListViewModel(Role model)
        {
            Id = model.Id;
            Name = model.Name;
            Description = model.Description;
            RolePermissions = model.Permissions;
        }

        public int Id { get; set; }
    }
}
