using System;
using System.Collections.Generic;
using System.Text;

namespace DevFactoryZ.CharityCRM.Persistence
{
    public interface IFundRegistrationRepository
    {
        int Create(FundRegistration entity);
        FundRegistration Read<TKey>(TKey key);
        bool Update(FundRegistration entity);
        void Delete(FundRegistration entity);
        IEnumerable<FundRegistration> FindBy(Func<FundRegistration, bool> predicate);

    }
}
