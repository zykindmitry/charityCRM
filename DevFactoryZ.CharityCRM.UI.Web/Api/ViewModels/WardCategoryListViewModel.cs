namespace DevFactoryZ.CharityCRM.UI.Web.Api.ViewModels
{
    public class WardCategoryListViewModel : WardCategoryViewModel
    {
        public WardCategoryListViewModel() : base()
        {
        }

        public WardCategoryListViewModel(WardCategory model)
        {
            Id = model.Id;
            Name = model.Name;
            SubCategories = model.SubCategories;
        }

        public int Id { get; set; }
    }
}
