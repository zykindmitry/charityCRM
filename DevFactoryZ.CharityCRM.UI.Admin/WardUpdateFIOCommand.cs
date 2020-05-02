using DevFactoryZ.CharityCRM.Persistence;
using System;
using System.Text;
using System.Linq;

namespace DevFactoryZ.CharityCRM.UI.Admin
{
    /// <summary>
    /// Имплементация <see cref="ICommand"/> для изменения ФИО подопечного БФ в хранилище.
    /// </summary>
    class WardUpdateFIOCommand : ICommand
    {
        private const char valueSeparator = ',';

        /// <summary>
        /// Создвет экземпляр <see cref="WardUpdateFIOCommand "/>.
        /// </summary>
        /// <param name="unitOfWorkCreator">Экземпляр <see cref="ICreateUnitOfWork"/> для работы с хранилищем.</param>
        public WardUpdateFIOCommand(ICreateUnitOfWork unitOfWorkCreator)
        {
            this.unitOfWorkCreator = unitOfWorkCreator;
        }

        private static string CommandText = "update-ward-fio";

        private static string Alias = "uwf";

        private static string IdParameter = "Id подопечного";

        private static string NewFIOParameter = $"Фамилия,Имя,Отчество - заключить в кавычки, разделять знаком '{valueSeparator}'.";

        public string Help =>
            (new StringBuilder($"Напишите '{CommandText} (или {Alias}) [{IdParameter}] [{NewFIOParameter}]', чтобы изменить ФИО подопечного. "))
            .AppendLine()
            .Append($"    Внимание!!! {IdParameter} можно узнать, выполнив команду 'list-wards'.")
            .ToString();
             
        private readonly ICreateUnitOfWork unitOfWorkCreator;

        public void Execute(string[] parameters)
        {
            if (parameters.Length < 2)
            {
                Console.WriteLine($"Ошибка! Отсутствует один или два обязательных параметра: '{IdParameter}', '{NewFIOParameter}'");
                return;
            }

            if (!int.TryParse(parameters[0], out int wardId))
            {
                Console.WriteLine($"Ошибка! Первый обязательный параметр '{IdParameter}' должен быть целым положительным числом.");
                return;
            }

            if (string.IsNullOrWhiteSpace(parameters[1]) || !parameters[1].Contains(valueSeparator))
            {
                Console.WriteLine($"Ошибка! Второй обязательный параметр '{NewFIOParameter}' - должен содержать хотя бы один символ.");
                Console.WriteLine($"Ошибка! Разделитель значенй в параметре должен быть '{valueSeparator}'.");
                return;
            }

            using (var unitOfWork = unitOfWorkCreator.Create())
            {
                var fioArray = parameters[1].Split(valueSeparator);
                int lastName = 0;
                int firstName = 1;
                int midName = 2;

                var ward = 
                    unitOfWork.GetById<Ward, int>(wardId); 

                ward.FIO = new FIO(fioArray[lastName], fioArray[firstName], fioArray[midName]);

                unitOfWork.Save();

                Console.WriteLine($"ФИО подопечного с идентификатором (ID = {ward.Id}) изменено.");
            }
        }

        public bool Recognize(string command)
        {
            return command == CommandText || command == Alias;
        }
    }
}
