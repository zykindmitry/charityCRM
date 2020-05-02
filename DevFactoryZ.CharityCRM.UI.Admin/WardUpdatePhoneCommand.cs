using DevFactoryZ.CharityCRM.Persistence;
using System;
using System.Text;
using System.Linq;

namespace DevFactoryZ.CharityCRM.UI.Admin
{
    /// <summary>
    /// Имплементация <see cref="ICommand"/> для изменения номера телефона подопечного БФ в хранилище.
    /// </summary>
    class WardUpdatePhoneCommand : ICommand
    {
        /// <summary>
        /// Создвет экземпляр <see cref="WardUpdatePhoneCommand "/>.
        /// </summary>
        /// <param name="unitOfWorkCreator">Экземпляр <see cref="ICreateUnitOfWork"/> для работы с хранилищем.</param>
        public WardUpdatePhoneCommand(ICreateUnitOfWork unitOfWorkCreator)
        {
            this.unitOfWorkCreator = unitOfWorkCreator;
        }

        private static string CommandText = "update-ward-phone";

        private static string Alias = "uwp";

        private static string IdParameter = "Id подопечного";

        private static string NewPhoneParameter = $"Новый номер телефона";

        public string Help =>
            (new StringBuilder($"Напишите '{CommandText} (или {Alias}) [{IdParameter}] ({NewPhoneParameter})', чтобы изменить номер телефона подопечного. "))
            .AppendLine()
            .Append($"    Внимание!!! {IdParameter} можно узнать, выполнив команду 'list-wards'.")
            .ToString();
             
        private readonly ICreateUnitOfWork unitOfWorkCreator;

        public void Execute(string[] parameters)
        {
            if (parameters.Length < 2)
            {
                Console.WriteLine($"Ошибка! Отсутствует один или два обязательных параметра: '{IdParameter}', '{NewPhoneParameter}'");
                return;
            }

            if (!int.TryParse(parameters[0], out int wardId))
            {
                Console.WriteLine($"Ошибка! Первый обязательный параметр '{IdParameter}' должен быть целым положительным числом.");
                return;
            }

            if (string.IsNullOrWhiteSpace(parameters[1]))
            {
                Console.WriteLine($"Ошибка! Втоой обязательный параметр '{NewPhoneParameter}' должен содержать хотя бы один символ.");
                return;
            }

            using (var unitOfWork = unitOfWorkCreator.Create())
            {
                var ward = 
                    unitOfWork.GetById<Ward, int>(wardId); 

                ward.Phone = parameters[1];

                unitOfWork.Save();

                Console.WriteLine($"Номер телефона подопечного с идентификатором (ID = {ward.Id}) изменен.");
            }
        }

        public bool Recognize(string command)
        {
            return command == CommandText || command == Alias;
        }
    }
}
