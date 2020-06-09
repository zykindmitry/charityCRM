using System.Collections.Generic;

namespace DevFactoryZ.CharityCRM.UI.Web.Api.ViewModels
{
    public class WardCategoryViewModel
    {
        public string Name { get; set; }
        public IEnumerable<WardCategory> SubCategories { get; set; }
    }
}
