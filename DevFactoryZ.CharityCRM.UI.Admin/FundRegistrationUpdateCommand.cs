using DevFactoryZ.CharityCRM.Persistence;
using System;
using System.Text;
using System.Linq;

namespace DevFactoryZ.CharityCRM.UI.Admin
{
    /// <summary>
    /// Имплементация <see cref="ICommand"/> для изменения наименования разрешения в хранилище.
    /// </summary>
    class FundRegistrationUpdateCommand : ICommand
    {
        /// <summary>
        /// Создвет экземпляр <see cref="FundRegistrationUpdateCommand"/>.
        /// </summary>
        /// <param name="unitOfWorkCreator">Экземпляр <see cref="ICreateUnitOfWork"/> для работы с хранилищем.</param>
        public FundRegistrationUpdateCommand(ICreateUnitOfWork unitOfWorkCreator)
        {
            this.unitOfWorkCreator = unitOfWorkCreator;
        }

        private static string CommandText = "update-fund_registration";

        private static string IdParameter = "Guid заявки";

        private static string NewDescriptionParameter = "Новое описание фонда";

        public string Help =>
            (new StringBuilder($"Напишите '{CommandText} [{IdParameter}] [{NewDescriptionParameter}]', чтобы изменить описание фонда. "))
            .AppendLine()
            .Append($"    Внимание!!! {IdParameter} можно узнать, выполнив команду 'list-fund_registration'.")
            .ToString();
             
        private readonly ICreateUnitOfWork unitOfWorkCreator;

        public void Execute(string[] parameters)
        {
            if (parameters.Length < 2)
            {
                Console.WriteLine($"Ошибка! Отсутствует один или два обязательных параметра: '{IdParameter}', '{NewDescriptionParameter}'");
                return;
            }

            if (!Guid.TryParse(parameters[0], out Guid fundRegistrationId))
            {
                Console.WriteLine($"Ошибка! Первый обязательный параметр '{IdParameter}' должен быть в формата GUID.");
                return;
            }

            if (string.IsNullOrWhiteSpace(parameters[1]))
            {
                Console.WriteLine($"Ошибка! Третий обязательный параметр '{NewDescriptionParameter}' должен содержать хотя бы один символ.");
                return;
            }

            using (var unitOfWork = unitOfWorkCreator.Create())
            {
                var fundRegistration = 
                    unitOfWork.GetById<FundRegistration, Guid>(fundRegistrationId);

                fundRegistration.Description = String.Join(' ', parameters.Skip(1).Select(x => x));
                unitOfWork.Save();

                Console.WriteLine($"Информация о регистрируемом фонде с идентификатором (ID = {fundRegistration.Id}) изменена.");
            }

        }

        public bool Recognize(string command)
        {
            return command == CommandText;
        }
    }
}
