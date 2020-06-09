using DevFactoryZ.CharityCRM.Persistence;
using System;
using System.Linq;

namespace DevFactoryZ.CharityCRM.UI.Admin
{
    class WardCategoryCreateCommand : ICommand
    {
        public WardCategoryCreateCommand(ICreateUnitOfWork unitOfWorkCreator)
        {
            this.unitOfWorkCreator = unitOfWorkCreator;
        }

        private static string CommandText = "create-ward-category";

        private static string Alias = "cwc";

        private static string NameParameter = "Наименование категории подопечного";

        public string Help => 
            $"Напишите '{CommandText} (или {Alias}) [{NameParameter}]', чтобы создать категорию подопечного";

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
                var wardCategory = 
                    new WardCategory(parameters.First());

                unitOfWork.Add(wardCategory);
                unitOfWork.Save();

                Console.WriteLine($"Категория подопечного {wardCategory.Name} создана с идентификатором (ID = {wardCategory.Id})");
            }
        }

        public bool Recognize(string command)
        {
            return command == CommandText || command == Alias;
        }
    }
}
