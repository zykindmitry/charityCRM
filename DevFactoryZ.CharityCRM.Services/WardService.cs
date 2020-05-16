using DevFactoryZ.CharityCRM.Persistence;
using System.Collections.Generic;

namespace DevFactoryZ.CharityCRM.Services
{
    public class WardService : IWardService
    {
        private readonly IWardRepository repository;

        public WardService(IWardRepository repository)
        {
            this.repository = repository;
        }

        public Ward Create(WardData data)
        {
            var ward = new Ward(
                data.FullName, 
                data.Address, 
                data.BirthDate, 
                data.Phone, 
                data.WardCategories);

            repository.Add(ward);

            repository.Save();

            return ward;
        }

        public void Delete(int id)
        {
            repository.Delete(id);
            repository.Save();
        }

        public IEnumerable<Ward> GetAll()
        {
            return repository.GetAll();
        }

        public Ward GetById(int id)
        {
            return repository.GetById(id);
        }

        public void Update(int id, WardData data)
        {
            var ward = repository.GetById(id);

            ward.FullName.FirstName = data.FullName.FirstName;
            ward.FullName.MiddleName = data.FullName.MiddleName;
            ward.FullName.SurName = data.FullName.SurName;
            
            ward.Address.PostCode = data.Address.PostCode;
            ward.Address.Country = data.Address.Country;
            ward.Address.Region = data.Address.Region;
            ward.Address.City = data.Address.City;
            ward.Address.Area = data.Address.Area;
            ward.Address.Street = data.Address.Street;
            ward.Address.House = data.Address.House;
            ward.Address.Flat = data.Address.Flat;
            
            ward.BirthDate = data.BirthDate;
            ward.Phone = data.Phone;

            ward.WardCategories.Clear();

            data.WardCategories?.Each(c => ward.AddCategory(c));
            
            repository.Save();
        }
    }
}
