namespace DevFactoryZ.CharityCRM.UI.Web.Api.ViewModels
{
    public class PermissionListViewModel : PermissionViewModel
    {
        public PermissionListViewModel() : base()
        {
        }

        public PermissionListViewModel(Permission model)
        {
            Id = model.Id;
            Name = model.Name;
            Description = model.Description;
        }

        public int Id { get; set; }
    }
}
