using DevFactoryZ.CharityCRM.Services;
using System.Collections.Generic;
using static DevFactoryZ.CharityCRM.Ward;
using System.Linq;
using System;

namespace DevFactoryZ.CharityCRM.UI.Web.Api.ViewModels
{
    public class WardViewModel
    {
        public IFIO FIO { get; set; }

        public IAddress Address { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Phone { get; set; }

        public IEnumerable<ThisWardCategory> ThisWardCategories { get; set; }

        public WardData ToDto()
        {
            return new WardData
            {
                FIO = FIO,
                Address = Address,
                BirthDate = BirthDate,
                Phone = Phone,
                ThisWardCategories = ThisWardCategories.Select(thisWardCategories => 
                    new WardCategory(thisWardCategories.WardCategory.Name))
            };
        }
    }
}
