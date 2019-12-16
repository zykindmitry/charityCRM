using DevFactoryZ.CharityCRM.Persistence;
using System;
using System.Text;
using System.Linq;

namespace DevFactoryZ.CharityCRM.UI.Admin
{
    /// <summary>
    /// Имплементация <see cref="ICommand"/> для добавления разрешения в список разрешений для роли.
    /// </summary>
    class RoleAddPermissionCommand : ICommand
    {
        /// <summary>
        /// Создвет экземпляр <see cref="RoleAddPermissionCommand"/>.
        /// </summary>
        /// <param name="unitOfWorkCreator">Экземпляр <see cref="ICreateUnitOfWork"/> для работы с хранилищем.</param>
        public RoleAddPermissionCommand(ICreateUnitOfWork unitOfWorkCreator)
        {
            this.unitOfWorkCreator = unitOfWorkCreator;
        }

        private static string CommandText = "update-role-add-permission";

        private static string IdRoleParameter = "Id роли";

        private static string IdPermissionParameter = "Id разрешения";

        public string Help =>
            (new StringBuilder($"Напишите '{CommandText} [{IdRoleParameter}] [{IdPermissionParameter}]', чтобы добавить разрешение для роли. "))
            .AppendLine()
            .AppendLine($"    Внимание!!! {IdRoleParameter} можно узнать, выполнив команду 'list-roles'.")
            .Append($"    Внимание!!! {IdPermissionParameter} можно узнать, выполнив команду 'list-permissions'.")
            .ToString();
             
        private readonly ICreateUnitOfWork unitOfWorkCreator;

        public void Execute(string[] parameters)
        {
            if (parameters.Length < 2)
            {
                Console.WriteLine($"Ошибка! Отсутствует один или два обязательных параметра: '{IdRoleParameter}', '{IdPermissionParameter}'");
                return;
            }

            if (!int.TryParse(parameters[0], out int roleId))
            {
                Console.WriteLine($"Ошибка! Первый обязательный параметр '{IdRoleParameter}' должен быть целым положительным числом.");
                return;
            }

            if (!int.TryParse(parameters[1], out int permissionId))
            {
                Console.WriteLine($"Ошибка! Второй обязательный параметр '{IdPermissionParameter}' должен быть целым положительным числом.");
                return;
            }

            using (var unitOfWork = unitOfWorkCreator.Create())
            {
                var role = 
                    unitOfWork.GetById<Role, int>(roleId);

                var permission =
                    unitOfWork.GetById<Permission, int>(permissionId);

                role.Grant(permission);
                unitOfWork.Save();

                Console.WriteLine($"Разрешение '{permission.Name}' добавлено к роли '{role.Name}'.");
            }

        }

        public bool Recognize(string command)
        {
            return command == CommandText;
        }
    }
}
