using System.Collections.Immutable;
using System.Collections.Generic;
using System.Linq;

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
            SubCategories = model.SubCategories.Select(s => s.WardCategory);
        }

        public int Id { get; set; }
    }
}
