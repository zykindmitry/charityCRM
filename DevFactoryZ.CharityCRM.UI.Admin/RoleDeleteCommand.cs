using DevFactoryZ.CharityCRM.Persistence;
using System;
using System.Text;
using System.Linq;

namespace DevFactoryZ.CharityCRM.UI.Admin
{
    /// <summary>
    /// Имплементация <see cref="ICommand"/> для удаления роли из хранилища.
    /// </summary>
    class RoleDeleteCommand : ICommand
    {
        /// <summary>
        /// Создвет экземпляр <see cref="RoleDeleteCommand"/>.
        /// </summary>
        /// <param name="unitOfWorkCreator">Экземпляр <see cref="ICreateUnitOfWork"/> для работы с хранилищем.</param>
        public RoleDeleteCommand(ICreateUnitOfWork unitOfWorkCreator)
        {
            this.unitOfWorkCreator = unitOfWorkCreator;
        }

        private static string CommandText = "delete-role";

        private static string IdParameter = "Id роли";

        public string Help =>
            (new StringBuilder($"Напишите '{CommandText} [{IdParameter}]', чтобы удалить роль. "))
            .AppendLine()
            .Append($"    Внимание!!! {IdParameter} можно узнать, выполнив команду 'list-roles'.")
            .ToString();
             
        private readonly ICreateUnitOfWork unitOfWorkCreator;

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

            using (var unitOfWork = unitOfWorkCreator.Create())
            {
                var role = 
                    unitOfWork.GetById<Role, int>(roleId);

                if (role == null)
                {
                    Console.WriteLine($"Ошибка! В хранилище отсутствует роль с идентификатором (ID = {roleId}).");
                    return;
                }

                unitOfWork.Remove(role);
                unitOfWork.Save();

                Console.WriteLine($"Роль с идентификатором (ID = {roleId}) удалена из хранилища.");
            }
        }

        public bool Recognize(string command)
        {
            return command == CommandText;
        }
    }
}
