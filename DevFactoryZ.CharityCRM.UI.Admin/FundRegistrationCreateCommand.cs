using DevFactoryZ.CharityCRM.Persistence;
using System;
using System.Linq;

namespace DevFactoryZ.CharityCRM.UI.Admin
{
    class FundRegistrationCreateCommand : ICommand
    {
        public FundRegistrationCreateCommand(ICreateUnitOfWork unitOfWorkCreator)
        {
            this.unitOfWorkCreator = unitOfWorkCreator;
        }

        private static string CommandText = "create-fund_regisration";

        private static string NameParameter = "Наименование фонда";

        private static string DescriptionParameter = "Краткое описание к заявке";

        public string Help => 
            $"Напишите '{CommandText} [{NameParameter}] [{DescriptionParameter}]', чтобы создать заявку на регистрацию фонда";

        private readonly ICreateUnitOfWork unitOfWorkCreator;

        public void Execute(string[] parameters)
        {
            if (!parameters.Any())
            {
                Console.WriteLine($"Ошибка! Отсутствует обязательный параметр '{NameParameter}'");
                return;
            }

            using(var unitOfWork = unitOfWorkCreator.Create())
            {
                var fundRegistration = 
                    new FundRegistration(parameters.First(), parameters.Skip(1).First(), new TimeSpan(2,0,0));

                unitOfWork.Add(fundRegistration);
                unitOfWork.Save();

                Console.WriteLine($"Заявка на регистрацию фонда {fundRegistration.Name} ({fundRegistration.Description}) создано с идентификатором (ID = {fundRegistration.Id})");
            }
        }

        public bool Recognize(string command)
        {
            return command == CommandText;
        }
    }
}
