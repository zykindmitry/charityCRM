using DevFactoryZ.CharityCRM.Persistence;
using System;
using System.Text;
using System.Linq;

namespace DevFactoryZ.CharityCRM.UI.Admin
{
    /// <summary>
    /// Имплементация <see cref="ICommand"/> для удаления разрешения из хранилища.
    /// </summary>
    class PermissionDeleteCommand : ICommand
    {
        /// <summary>
        /// Создвет экземпляр <see cref="PermissionDeleteCommand"/>.
        /// </summary>
        /// <param name="unitOfWorkCreator">Экземпляр <see cref="ICreateUnitOfWork"/> для работы с хранилищем.</param>
        public PermissionDeleteCommand(ICreateUnitOfWork unitOfWorkCreator)
        {
            this.unitOfWorkCreator = unitOfWorkCreator;
        }

        private static string CommandText = "delete-permission";

        private static string IdParameter = "Id разрешения";

        public string Help =>
            (new StringBuilder($"Напишите '{CommandText} [{IdParameter}]', чтобы удалить разрешение. "))
            .AppendLine()
            .Append($"    Внимание!!! {IdParameter} можно узнать, выполнив команду 'list-permissions'.")
            .ToString();
             
        private readonly ICreateUnitOfWork unitOfWorkCreator;

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

            using (var unitOfWork = unitOfWorkCreator.Create())
            {
                var permission = 
                    unitOfWork.GetById<Permission, int>(permissionId);

                unitOfWork.Remove(permission);
                unitOfWork.Save();

                Console.WriteLine($"Разрешение с идентификатором (ID = {permissionId}) удалено из хранилища.");
            }
        }

        public bool Recognize(string command)
        {
            return command == CommandText;
        }
    }
}
