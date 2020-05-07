using DevFactoryZ.CharityCRM.Services;
using System.Collections.Generic;
using static DevFactoryZ.CharityCRM.Ward;
using System.Linq;
using System;

namespace DevFactoryZ.CharityCRM.UI.Web.Api.ViewModels
{
    public class WardViewModel
    {
        public FullName FullName { get; set; }

        public Address Address { get; set; }

        public DateTime BirthDate { get; set; }

        public DateTime CreatedAt { get; set; }

        public string Phone { get; set; }

        public IEnumerable<WardCategory> WardCategories { get; set; }

        public WardData ToDto()
        {
            return new WardData
            {
                FullName = FullName,
                Address = Address,
                BirthDate = BirthDate,
                Phone = Phone,
                WardCategories = WardCategories
            };
        }
    }
}
