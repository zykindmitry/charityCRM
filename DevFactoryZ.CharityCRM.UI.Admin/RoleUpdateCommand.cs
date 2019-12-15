using DevFactoryZ.CharityCRM.Persistence;
using System;
using System.Text;
using System.Linq;

namespace DevFactoryZ.CharityCRM.UI.Admin
{
    /// <summary>
    /// Имплементация <see cref="ICommand"/> для изменения наименования роли в хранилище.
    /// </summary>
    class RoleUpdateCommand : ICommand
    {
        /// <summary>
        /// Создвет экземпляр <see cref="RoleUpdateCommand"/>.
        /// </summary>
        /// <param name="unitOfWorkCreator">Экземпляр <see cref="ICreateUnitOfWork"/> для работы с хранилищем.</param>
        public RoleUpdateCommand(ICreateUnitOfWork unitOfWorkCreator)
        {
            this.unitOfWorkCreator = unitOfWorkCreator;
        }

        private static string CommandText = "update-role";

        private static string IdParameter = "Id роли";

        private static string NewNameParameter = "Новое наименование роли";

        public string Help =>
            (new StringBuilder($"Напишите '{CommandText} [{IdParameter}] [{NewNameParameter}]', чтобы изменить наименование роли. "))
            .AppendLine()
            .Append($"    Внимание!!! {IdParameter} можно узнать, выполнив команду 'list-roles'.")
            .ToString();
             
        private readonly ICreateUnitOfWork unitOfWorkCreator;

        public void Execute(string[] parameters)
        {
            if (parameters.Length < 2)
            {
                Console.WriteLine($"Ошибка! Отсутствует один или два обязательных параметра: '{IdParameter}', '{NewNameParameter}'");
                return;
            }

            if (!int.TryParse(parameters[0], out int roleId))
            {
                Console.WriteLine($"Ошибка! Первый обязательный параметр '{IdParameter}' должен быть целым положительным числом.");
                return;
            }

            if (string.IsNullOrWhiteSpace(parameters[1]))
            {
                Console.WriteLine($"Ошибка! Второй обязательный параметр '{NewNameParameter}' должен содержать хотя бы один символ.");
                return;
            }

            using (var unitOfWork = unitOfWorkCreator.Create())
            {
                var role = 
                    unitOfWork.GetById<Role, int>(roleId); 

                if (role == null)
                {
                    Console.WriteLine($"Ошибка! В хранилище отсутствует роль с идентификатором (ID = {roleId}).");
                    return;
                }

                role.ChangeNameTo(parameters[1]);
                unitOfWork.Save();

                Console.WriteLine($"Наименование роли с идентификатором (ID = {role.Id}) изменено.");
            }

        }

        public bool Recognize(string command)
        {
            return command == CommandText;
        }
    }
}
