using System;
using System.Collections.Generic;
using System.Text;

namespace DevFactoryZ.CharityCRM
{
    public class Address : IAddress
    {
        #region .ctor

        public Address() //For ORM
        {

        }

        public Address(
            string postCode
            , string country
            , string region
            , string city
            , string area
            , string street
            , string house
            , string flat)
            : this()
        {
            PostCode = postCode;
            Country = country;
            Region = region;
            City = city;
            Area = area;
            Street = street;
            House = house;
            Flat = flat;
        }

        #endregion

        #region Поля и свойства

        public string PostCode { get; private set; }

        public string Country { get; private set; }

        public string Region { get; private set; }

        public string City { get; private set; }

        public string Area { get; private set; }

        public string Street { get; private set; }

        public string House { get; private set; }

        public string Flat { get; private set; }

        #endregion

        #region Методы

        public void Update(IAddress newAddress)
        {
            if (newAddress == null)
            {
                throw new ArgumentNullException(nameof(newAddress));
            }

            PostCode = newAddress.PostCode;
            Country = newAddress.Country;
            Region = newAddress.Region;
            City = newAddress.City;
            Area = newAddress.Area;
            Street = newAddress.Street;
            House = newAddress.House;
            Flat = newAddress.Flat;
        }

        #endregion

        #region Переопределенные методы

        public override string ToString()
        {
            return $"{Country}, {Region}, {City}, {Area}, {Street}, {House}, {Flat}";
        }

        #endregion
    }
}
