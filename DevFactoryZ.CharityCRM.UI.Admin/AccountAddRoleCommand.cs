using DevFactoryZ.CharityCRM.Persistence;
using System;
using System.Text;
using System.Linq;

namespace DevFactoryZ.CharityCRM.UI.Admin
{
    /// <summary>
    /// Имплементация <see cref="ICommand"/> для добавления роли аккаунту.
    /// </summary>
    class AccountAddRoleCommand : ICommand
    {
        /// <summary>
        /// Создвет экземпляр <see cref="AccountAddRoleCommand"/>.
        /// </summary>
        /// <param name="unitOfWorkCreator">Экземпляр <see cref="ICreateUnitOfWork"/> для работы с хранилищем.</param>
        public AccountAddRoleCommand(ICreateUnitOfWork unitOfWorkCreator)
        {
            this.unitOfWorkCreator = unitOfWorkCreator;
        }

        private static string CommandText = "update-account-add-role";

        private static string IdRoleParameter = "Id роли";

        private static string IdAccountParameter = "Id аккаунта";

        public string Help =>
            (new StringBuilder($"Напишите '{CommandText} [{IdAccountParameter}] [{IdRoleParameter}]', чтобы добавить роль аккаунту. "))
            .AppendLine()
            .Append($"    Внимание!!! {IdRoleParameter} можно узнать, выполнив команду 'list-roles'.")
            .ToString();

        private readonly ICreateUnitOfWork unitOfWorkCreator;

        public void Execute(string[] parameters)
        {
            if (parameters.Length < 2)
            {
                Console.WriteLine($"Ошибка! Отсутствует один или два обязательных параметра: '{IdAccountParameter}', '{IdRoleParameter}'");
                return;
            }

            if (!int.TryParse(parameters[0], out int accountId))
            {
                Console.WriteLine($"Ошибка! Первый обязательный параметр '{IdAccountParameter}' должен быть целым положительным числом.");
                return;
            }

            if (!int.TryParse(parameters[1], out int roleId))
            {
                Console.WriteLine($"Ошибка! Второй обязательный параметр '{IdRoleParameter}' должен быть целым положительным числом.");
                return;
            }

            using (var unitOfWork = unitOfWorkCreator.Create())
            {
                Account account =
                    unitOfWork.GetById<Account, int>(accountId);

                var role =
                    unitOfWork.GetById<Role, int>(roleId);

                account.Role = role;
                unitOfWork.Save();

                Console.WriteLine($"Аккаунту '{account.Login}' добавлена к роль '{role.Name}'.");
            }

        }

        public bool Recognize(string command)
        {
            return command == CommandText;
        }
    }
}
