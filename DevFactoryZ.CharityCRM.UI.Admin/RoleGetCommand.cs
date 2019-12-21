using DevFactoryZ.CharityCRM.Persistence;
using System;
using System.Text;
using System.Linq;

namespace DevFactoryZ.CharityCRM.UI.Admin
{
    /// <summary>
    /// Имплементация <see cref="ICommand"/> для получения роли по ее Id из хранилища.
    /// </summary>
    class RoleGetCommand : ICommand
    {
        /// <summary>
        /// Создвет экземпляр <see cref="RoleGetCommand"/>.
        /// </summary>
        /// <param name="repositoryCreator">Экземпляр <see cref="IRoleRepository"/> для работы с хранилищем.</param>
        public RoleGetCommand(ICreateRepository<IRoleRepository> repositoryCreator)
        {
            this.repositoryCreator = repositoryCreator;
        }

        private static string CommandText = "get-role";

        private static string IdParameter = "Id роли";

        public string Help =>
            (new StringBuilder($"Напишите '{CommandText} [{IdParameter}]', чтобы получить роль. "))
            .AppendLine()
            .Append($"    Внимание!!! {IdParameter} можно узнать, выполнив команду 'list-roles'.")
            .ToString();

        private readonly ICreateRepository<IRoleRepository> repositoryCreator;

        public void Execute(string[] parameters)
        {
            if (!parameters.Any())
            {
                Console.WriteLine($"Ошибка! Отсутствует обязательный параметр '{IdParameter}'");
                return;
            }

            if (!int.TryParse(parameters.First(), out int roleId))
            {
                Console.WriteLine($"Ошибка! Обязательный параметр '{IdParameter}' должен быть целым положительным числом.");
                return;
            }

            var repository = repositoryCreator.Create();
            var role = repository.GetById(roleId);

            Console.WriteLine($"{nameof(Role.Name)}: {role.Name}.");
            Console.WriteLine($"{nameof(Role.Description)}: {(string.IsNullOrWhiteSpace(role.Description) ? "<empty>" : role.Description)}.");
            Console.WriteLine("Разрешения:");

            if (role.Permissions.Count() > 0)
            {
                Console.WriteLine($"{nameof(Permission.Id),10} {nameof(Permission.Name)}");

                foreach (var rolePermission in role.Permissions)
                {
                    Console.WriteLine("{0,10:0} {1}", rolePermission.Permission.Id, rolePermission.Permission.Name);
                }
            }
            else
            {
                Console.WriteLine($"{"<empty>", 15}");
            }
        }

        public bool Recognize(string command)
        {
            return command == CommandText;
        }
    }
}
