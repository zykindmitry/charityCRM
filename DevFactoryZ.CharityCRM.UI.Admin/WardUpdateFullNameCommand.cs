using DevFactoryZ.CharityCRM.Persistence;
using System;
using System.Text;
using System.Linq;

namespace DevFactoryZ.CharityCRM.UI.Admin
{
    /// <summary>
    /// Имплементация <see cref="ICommand"/> для изменения ФИО подопечного БФ в хранилище.
    /// </summary>
    class WardUpdateFullNameCommand : ICommand
    {
        private const char valueSeparator = ',';

        /// <summary>
        /// Создвет экземпляр <see cref="WardUpdateFullNameCommand "/>.
        /// </summary>
        /// <param name="unitOfWorkCreator">Экземпляр <see cref="ICreateUnitOfWork"/> для работы с хранилищем.</param>
        public WardUpdateFullNameCommand(ICreateUnitOfWork unitOfWorkCreator)
        {
            this.unitOfWorkCreator = unitOfWorkCreator;
        }

        private static string CommandText = "update-ward-fullname";

        private static string Alias = "uwfn";

        private static string IdParameter = "Id подопечного";

        private static string NewFullNameParameter = $"Фамилия,Имя,Отчество - без пробелов, компоненты разделять знаком '{valueSeparator}'.";

        public string Help =>
            (new StringBuilder($"Напишите '{CommandText} (или {Alias}) [{IdParameter}] [{NewFullNameParameter}]', чтобы изменить ФИО подопечного. "))
            .AppendLine()
            .Append($"    Внимание!!! {IdParameter} можно узнать, выполнив команду 'list-wards' или 'lw'.")
            .ToString();
             
        private readonly ICreateUnitOfWork unitOfWorkCreator;

        public void Execute(string[] parameters)
        {
            if (parameters.Length < 2)
            {
                Console.WriteLine($"Ошибка! Отсутствует один или два обязательных параметра: '{IdParameter}', '{NewFullNameParameter}'");
                return;
            }

            if (!int.TryParse(parameters[0], out int wardId))
            {
                Console.WriteLine($"Ошибка! Первый обязательный параметр '{IdParameter}' должен быть целым положительным числом.");
                return;
            }

            if (string.IsNullOrWhiteSpace(parameters[1]) || !parameters[1].Contains(valueSeparator))
            {
                Console.WriteLine($"Ошибка! Второй обязательный параметр '{NewFullNameParameter}' - должен содержать хотя бы один символ.");
                Console.WriteLine($"Ошибка! Разделитель значенй в параметре должен быть '{valueSeparator}'.");
                return;
            }

            using (var unitOfWork = unitOfWorkCreator.Create())
            {
                var fullNameArray = parameters[1].Split(valueSeparator);
                int surName = 0;
                int firstName = 1;
                int middleName = 2;

                var ward = 
                    unitOfWork.GetById<Ward, int>(wardId); 

                ward.FullName.Update( new FullName(fullNameArray[surName], fullNameArray[firstName], fullNameArray[middleName]));

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
