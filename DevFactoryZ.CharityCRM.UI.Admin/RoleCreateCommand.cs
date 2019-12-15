using DevFactoryZ.CharityCRM.Persistence;
using System;
using System.Linq;

namespace DevFactoryZ.CharityCRM.UI.Admin
{
    /// <summary>
    /// Имплементация <see cref="ICommand"/> для создания роли.
    /// </summary>
    class RoleCreateCommand : ICommand
    {
        /// <summary>
        /// Создвет экземпляр <see cref="RoleCreateCommand"/>. 
        /// </summary>
        /// <param name="unitOfWorkCreator"></param>
        public RoleCreateCommand(ICreateUnitOfWork unitOfWorkCreator)
        {
            this.unitOfWorkCreator = unitOfWorkCreator;
        }

        private static string CommandText = "create-role";

        private static string NameParameter = "Наименование роли";

        public string Help => 
            $"Напишите '{CommandText} [{NameParameter}]', чтобы создать роль";

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
                var role = 
                    new Role(parameters.First(), string.Empty, new Permission[0]);

                unitOfWork.Add(role);
                unitOfWork.Save();

                Console.WriteLine($"Роль {role.Name} создана с идентификатором (ID = {role.Id})");
            }
        }

        public bool Recognize(string command)
        {
            return command == CommandText;
        }
    }
}
