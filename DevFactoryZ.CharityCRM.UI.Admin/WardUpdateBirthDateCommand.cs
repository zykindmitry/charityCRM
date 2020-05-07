using DevFactoryZ.CharityCRM.Persistence;
using System;
using System.Text;
using System.Linq;

namespace DevFactoryZ.CharityCRM.UI.Admin
{
    /// <summary>
    /// Имплементация <see cref="ICommand"/> для изменения даты рождения подопечного БФ в хранилище.
    /// </summary>
    class WardUpdateBirthDateCommand : ICommand
    {
        /// <summary>
        /// Создвет экземпляр <see cref="WardUpdateBirthDateCommand "/>.
        /// </summary>
        /// <param name="unitOfWorkCreator">Экземпляр <see cref="ICreateUnitOfWork"/> для работы с хранилищем.</param>
        public WardUpdateBirthDateCommand(ICreateUnitOfWork unitOfWorkCreator)
        {
            this.unitOfWorkCreator = unitOfWorkCreator;
        }

        private static string CommandText = "update-ward-birthdate";

        private static string Alias = "uwbd";

        private static string IdParameter = "Id подопечного";

        private static string NewBirthDateParameter = $"Новая дата рождения";

        public string Help =>
            (new StringBuilder($"Напишите '{CommandText} (или {Alias}) [{IdParameter}] [{NewBirthDateParameter}]', чтобы изменить дату рождения подопечного. "))
            .AppendLine()
            .Append($"    Внимание!!! {IdParameter} можно узнать, выполнив команду 'list-wards' или 'lw'.")
            .ToString();
             
        private readonly ICreateUnitOfWork unitOfWorkCreator;

        public void Execute(string[] parameters)
        {
            if (parameters.Length < 2)
            {
                Console.WriteLine($"Ошибка! Отсутствует один или два обязательных параметра: '{IdParameter}', '{NewBirthDateParameter}'");
                return;
            }

            if (!int.TryParse(parameters[0], out int wardId))
            {
                Console.WriteLine($"Ошибка! Первый обязательный параметр '{IdParameter}' должен быть целым положительным числом.");
                return;
            }

            if (!DateTime.TryParse(parameters[1], out DateTime birthDate))
            {
                Console.WriteLine($"Ошибка! Неправильный формат даты во втором обязательном параметре '{NewBirthDateParameter}'");
                return;
            }

            using (var unitOfWork = unitOfWorkCreator.Create())
            {
                var ward = 
                    unitOfWork.GetById<Ward, int>(wardId); 

                ward.BirthDate = birthDate;

                unitOfWork.Save();

                Console.WriteLine($"Дата рождения подопечного с идентификатором (ID = {ward.Id}) изменена.");
            }
        }

        public bool Recognize(string command)
        {
            return command == CommandText || command == Alias;
        }
    }
}
