using System.Linq;

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
            FullName = model.FullName;
            Address = model.Address;
            BirthDate = model.BirthDate;
            Phone = model.Phone;
            CreatedAt = model.CreatedAt;
            WardCategories = model.WardCategories.Select(s => s.WardCategory);
        }

        public int Id { get; set; }

        public int? CategoryId { get; set; }
    }
}
