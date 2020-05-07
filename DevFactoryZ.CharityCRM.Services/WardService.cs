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
            var ward = new Ward(data.FullName, data.Address, data.BirthDate, data.Phone, data.WardCategories);
            repository.Add(ward);
            repository.Save();

            return ward;
        }

        public void Delete(int id)
        {
            repository.Delete(id);
            repository.Save();
        }

        public void Deny(int id, WardCategory wardCategory)
        {
            var ward = repository.GetById(id);
            ward.Deny(wardCategory);

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

        public void Grant(int id, WardCategory wardCategory)
        {
            var ward = repository.GetById(id);
            ward.Grant(wardCategory);

            repository.Save();
        }

        public void Update(int id, WardData data)
        {
            var ward = repository.GetById(id);
            ward.FullName.Update( data.FullName);
            ward.Address.Update( data.Address);
            ward.BirthDate = data.BirthDate;
            ward.Phone = data.Phone;
            
            repository.Save();
        }
    }
}
