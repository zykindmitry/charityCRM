using DevFactoryZ.CharityCRM.Persistence;
using System;
using System.Text;
using System.Linq;

namespace DevFactoryZ.CharityCRM.UI.Admin
{
    /// <summary>
    /// Имплементация <see cref="ICommand"/> для получения разрешения по его Id из хранилища.
    /// </summary>
    class PermissionGetCommand : ICommand
    {
        /// <summary>
        /// Создвет экземпляр <see cref="PermissionGetCommand"/>.
        /// </summary>
        /// <param name="permissionRepository">Экземпляр <see cref="IPermissionRepository"/> для работы с хранилищем.</param>
        public PermissionGetCommand(IPermissionRepository permissionRepository)
        {
            this.permissionRepository = permissionRepository;
        }

        private static string CommandText = "get-permission";

        private static string IdParameter = "Id разрешения";

        public string Help =>
            (new StringBuilder($"Напишите '{CommandText} [{IdParameter}]', чтобы получить разрешение. "))
            .AppendLine()
            .Append($"    Внимание!!! {IdParameter} можно узнать, выполнив команду 'list-permissions'.")
            .ToString();

        private readonly IPermissionRepository permissionRepository;

        public void Execute(string[] parameters)
        {
            if (!parameters.Any())
            {
                Console.WriteLine($"Ошибка! Отсутствует обязательный параметр '{IdParameter}'");
                return;
            }

            if (!int.TryParse(parameters.First(), out int permissionId))
            {
                Console.WriteLine($"Ошибка! Обязательный параметр '{IdParameter}' должен быть целым положительным числом.");
                return;
            }

            var permission =
                permissionRepository.GetById(permissionId);

            Console.WriteLine($"{nameof(Permission.Name)}: {permission.Name}.");
            Console.WriteLine($"{nameof(Permission.Description)}: {(string.IsNullOrWhiteSpace(permission.Description) ? "<empty>" : permission.Description)}.");
        }

        public bool Recognize(string command)
        {
            return command == CommandText;
        }
    }
}
