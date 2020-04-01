using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore
{
    internal class FundRegistrationRepository : IFundRegistrationRepository
    {
        private readonly DbSet<FundRegistration> setOfFundRegistration;

        private readonly Action save;

        public FundRegistrationRepository(DbSet<FundRegistration> setOfFundRegistration, Action save)
        {
            this.setOfFundRegistration = setOfFundRegistration;
            this.save = save;
        }

        public void Add(FundRegistration fundRegistration)
        {
            setOfFundRegistration.Add(fundRegistration);
        }

        public void Delete(Guid id)
        {
            setOfFundRegistration.Remove(GetById(id));
        }

        public IEnumerable<FundRegistration> GetAll()
        {
            return setOfFundRegistration.ToArray();
        }

        public FundRegistration GetById(Guid id)
        {
            return setOfFundRegistration.Find(id) ?? throw new EntityNotFoundException(id, typeof(FundRegistration));
        }

        public void Save()
        {
            save();
        }
    }
}
