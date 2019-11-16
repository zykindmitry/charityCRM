using DevFactoryZ.CharityCRM.Persistence;
using System;
using System.Linq;

namespace DevFactoryZ.CharityCRM.UI.Admin
{
    class PermissionCreateCommand : ICommand
    {
        public PermissionCreateCommand(ICreateUnitOfWork unitOfWorkCreator)
        {
            this.unitOfWorkCreator = unitOfWorkCreator;
        }

        private static string CommandText = "create-permission";

        private static string NameParameter = "Имя разрешения";

        public string Help => 
            $"Напишите '{CommandText} [{NameParameter}]', чтобы создать разрешение";

        private readonly ICreateUnitOfWork unitOfWorkCreator;

        public void Execute(string[] parameters)
        {
            if (!parameters.Any())
            {
                Console.WriteLine($"Ошибка! Отсутствует обязательный параметр '{NameParameter}'");
                return;
            }

            using(var unitOfWork = unitOfWorkCreator.Create())
            {
                var permission = 
                    new Permission(parameters.First(), string.Empty);

                unitOfWork.Add(permission);
                unitOfWork.Save();

                Console.WriteLine($"Разрешение {permission.Name} создано с идентификатором (ID = {permission.Id})");
            }
        }

        public bool Recognize(string command)
        {
            return command == CommandText;
        }
    }
}
