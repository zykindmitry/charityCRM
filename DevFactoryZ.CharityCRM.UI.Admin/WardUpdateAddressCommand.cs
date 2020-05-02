using DevFactoryZ.CharityCRM.Persistence;
using System;
using System.Text;
using System.Linq;

namespace DevFactoryZ.CharityCRM.UI.Admin
{
    /// <summary>
    /// Имплементация <see cref="ICommand"/> для изменения адрес подопечного БФ в хранилище.
    /// </summary>
    class WardUpdateAddressCommand : ICommand
    {
        private const char valueSeparator = ',';

        /// <summary>
        /// Создвет экземпляр <see cref="WardUpdateAddressCommand "/>.
        /// </summary>
        /// <param name="unitOfWorkCreator">Экземпляр <see cref="ICreateUnitOfWork"/> для работы с хранилищем.</param>
        public WardUpdateAddressCommand(ICreateUnitOfWork unitOfWorkCreator)
        {
            this.unitOfWorkCreator = unitOfWorkCreator;
        }

        private static string CommandText = "update-ward-address";

        private static string Alias = "uwa";

        private static string IdParameter = "Id подопечного";

        private static string NewAddressParameter = $"Адрес - заключить в кавычки, разделять знаком '{valueSeparator}'.";

        public string Help =>
            (new StringBuilder($"Напишите '{CommandText} (или {Alias}) [{IdParameter}] [{NewAddressParameter}]', чтобы изменить адрес подопечного. "))
            .AppendLine()
            .Append($"    Внимание!!! НЕ ИСПОЛЬЗОВАТЬ!!! Эта команда находится в стадии разработки!!!")
            .Append($"    Внимание!!! {IdParameter} можно узнать, выполнив команду 'list-wards'.")
            .ToString();
             
        private readonly ICreateUnitOfWork unitOfWorkCreator;

        public void Execute(string[] parameters)
        {
            if (parameters.Length < 2)
            {
                Console.WriteLine($"Ошибка! Отсутствует один или два обязательных параметра: '{IdParameter}', '{NewAddressParameter}'");
                return;
            }

            if (!int.TryParse(parameters[0], out int wardId))
            {
                Console.WriteLine($"Ошибка! Первый обязательный параметр '{IdParameter}' должен быть целым положительным числом.");
                return;
            }

            if (string.IsNullOrWhiteSpace(parameters[1]) || !parameters[1].Contains(valueSeparator))
            {
                Console.WriteLine($"Ошибка! Второй обязательный параметр '{NewAddressParameter}' - должен содержать хотя бы один символ.");
                Console.WriteLine($"Ошибка! Разделитель значенй в параметре должен быть '{valueSeparator}'.");
                return;
            }

            using (var unitOfWork = unitOfWorkCreator.Create())
            {
                var ward = 
                    unitOfWork.GetById<Ward, int>(wardId); 

                ward.Address = new Address();

                unitOfWork.Save();

                Console.WriteLine($"Адрес подопечного с идентификатором (ID = {ward.Id}) изменен.");
            }
        }

        public bool Recognize(string command)
        {
            return command == CommandText || command == Alias;
        }
    }
}
