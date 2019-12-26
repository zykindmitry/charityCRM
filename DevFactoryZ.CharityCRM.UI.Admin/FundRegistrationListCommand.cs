using DevFactoryZ.CharityCRM.Persistence;
using System.Linq;
using System;

namespace DevFactoryZ.CharityCRM.UI.Admin
{
    /// <summary>
    /// Имплементация <see cref="ICommand"/> для получения списка ролей из хранилища.
    /// </summary>
    class FundRegistrationListCommand : ICommand
    {
        /// <summary>
        /// Создвет экземпляр <see cref="FundRegistrationListCommand"/>.
        /// </summary>
        /// <param name="repositoryCreator">Экземпляр <see cref="IFundRegistrationRepository"/> для работы с хранилищем.</param>
        public FundRegistrationListCommand(ICreateRepository<IFundRegistrationRepository> repositoryCreator)
        {
            this.repositoryCreator = repositoryCreator;
        }

        private static string CommandText = "list-fund_registration";
        private static string AllParameter = "-all";

        public string Help => 
            $"Напишите '{CommandText} [{AllParameter}]', чтобы получить список всех (-all) или активных (без -all) заявок на регистрацию фондов.";

        private readonly ICreateRepository<IFundRegistrationRepository> repositoryCreator;

        public void Execute(string[] parameters)
        {
            var repository = repositoryCreator.Create();
            var fundRegistrations = parameters.Length == 0 ? repository.GetAll().Where(x => x.CanBeSucceeded == true) : repository.GetAll();

            Console.WriteLine($"{nameof(FundRegistration.Id)} {nameof(FundRegistration.Name)} {nameof(FundRegistration.Description)}");

            foreach (var fondRegistration in fundRegistrations)
            {
                Console.WriteLine("{0} {1} {2}", fondRegistration.Id, fondRegistration.Name, fondRegistration.Description);
            }
        }

        public bool Recognize(string command)
        {
            return command == CommandText;
        }
    }
}
