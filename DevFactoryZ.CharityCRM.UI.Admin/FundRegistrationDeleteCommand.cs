using DevFactoryZ.CharityCRM.Persistence;
using System;
using System.Text;
using System.Linq;

namespace DevFactoryZ.CharityCRM.UI.Admin
{
    /// <summary>
    /// Имплементация <see cref="ICommand"/> для удаления разрешения из хранилища.
    /// </summary>
    class FundRegistrationDeleteCommand : ICommand
    {
        /// <summary>
        /// Создвет экземпляр <see cref="FundRegistrationDeleteCommand"/>.
        /// </summary>
        /// <param name="unitOfWorkCreator">Экземпляр <see cref="ICreateUnitOfWork"/> для работы с хранилищем.</param>
        public FundRegistrationDeleteCommand(ICreateUnitOfWork unitOfWorkCreator)
        {
            this.unitOfWorkCreator = unitOfWorkCreator;
        }

        private static string CommandText = "delete-fund_registration";

        private static string IdParameter = "Guid заявления";

        public string Help =>
            (new StringBuilder($"Напишите '{CommandText} [{IdParameter}]', чтобы удалить заявление на регистрацию. "))
            .AppendLine()
            .Append($"    Внимание!!! {IdParameter} можно узнать, выполнив команду 'list-fund_registration'.")
            .ToString();
             
        private readonly ICreateUnitOfWork unitOfWorkCreator;

        public void Execute(string[] parameters)
        {
            if (!parameters.Any())
            {
                Console.WriteLine($"Ошибка! Отсутствует обязательный параметр '{IdParameter}'");
                return;
            }

            if (!Guid.TryParse(parameters.First(), out Guid fundRegistrationID))
            {
                Console.WriteLine($"Ошибка! Обязательный параметр '{IdParameter}' должен быть целым положительным числом.");
                return;
            }

            using (var unitOfWork = unitOfWorkCreator.Create())
            {
                var fundRegistration = 
                    unitOfWork.GetById<FundRegistration, Guid>(fundRegistrationID);

                unitOfWork.Remove(fundRegistration);
                unitOfWork.Save();

                Console.WriteLine($"Заявление на регистрацию с идентификатором (ID = {fundRegistrationID}) удалено из хранилища.");
            }
        }

        public bool Recognize(string command)
        {
            return command == CommandText;
        }
    }
}
