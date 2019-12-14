namespace DevFactoryZ.CharityCRM.UI.Web.Api.Models
{
    public class PermissionViewModel
    {
        public PermissionViewModel()
        {
        }

        public PermissionViewModel(Permission model)
        {
            Id = model.Id;
            Name = model.Name;
            Description = model.Description;
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
