namespace DevFactoryZ.CharityCRM.UI.Web.Api.ViewModels
{
    public class WardListViewModel : WardViewModel
    {
        public WardListViewModel() : base()
        {
        }

        public WardListViewModel(Ward model)
        {
            Id = model.Id;
            FIO = model.FIO;
            Address = model.Address;
            BirthDate = model.BirthDate;
            Phone = model.Phone;
            CreatedAt = model.CreatedAt;
            ThisWardCategories = ThisWardCategories;
        }

        public int Id { get; set; }
    }
}
