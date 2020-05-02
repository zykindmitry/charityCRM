using DevFactoryZ.CharityCRM.Persistence;
using System;
using System.Text;
using System.Linq;

namespace DevFactoryZ.CharityCRM.UI.Admin
{
    /// <summary>
    /// Имплементация <see cref="ICommand"/> для добавления подкатегории в список подкатегорий категории подопечного БФ.
    /// </summary>
    class WardCategoryAddSubCategoryCommand : ICommand
    {
        /// <summary>
        /// Создвет экземпляр <see cref="WardCategoryAddSubCategoryCommand"/>.
        /// </summary>
        /// <param name="unitOfWorkCreator">Экземпляр <see cref="ICreateUnitOfWork"/> для работы с хранилищем.</param>
        public WardCategoryAddSubCategoryCommand(ICreateUnitOfWork unitOfWorkCreator)
        {
            this.unitOfWorkCreator = unitOfWorkCreator;
        }

        private static string CommandText = "update-ward-category-add-subcategory";

        private static string Alias = "uwcasc";

        private static string IdWardCategoryParameter = "Id категории подопечного";

        private static string IdSubCategoryParameter = "Id подкатегории";

        public string Help =>
            (new StringBuilder($"Напишите '{CommandText} (или {Alias}) [{IdWardCategoryParameter}] [{IdSubCategoryParameter}]', чтобы добавить подкатегорию для категории подопечного. "))
            .AppendLine()
            .AppendLine($"    Внимание!!! {IdWardCategoryParameter} можно узнать, выполнив команду 'list-ward-categories'.")
            .Append($"    Внимание!!! {IdSubCategoryParameter} можно узнать, выполнив команду 'list-ward-categories'.")
            .ToString();
             
        private readonly ICreateUnitOfWork unitOfWorkCreator;

        public void Execute(string[] parameters)
        {
            if (parameters.Length < 2)
            {
                Console.WriteLine($"Ошибка! Отсутствует один или два обязательных параметра: '{IdWardCategoryParameter}', '{IdSubCategoryParameter}'");
                return;
            }

            if (!int.TryParse(parameters[0], out int wardCategoryId))
            {
                Console.WriteLine($"Ошибка! Первый обязательный параметр '{IdWardCategoryParameter}' должен быть целым положительным числом.");
                return;
            }

            if (!int.TryParse(parameters[1], out int subCategoryId))
            {
                Console.WriteLine($"Ошибка! Второй обязательный параметр '{IdSubCategoryParameter}' должен быть целым положительным числом.");
                return;
            }

            using (var unitOfWork = unitOfWorkCreator.Create())
            {
                var wardCategory = 
                    unitOfWork.GetById<Role, int>(wardCategoryId);

                var subCategory =
                    unitOfWork.GetById<Permission, int>(subCategoryId);

                wardCategory.Grant(subCategory);
                unitOfWork.Save();

                Console.WriteLine($"Подкатегория '{subCategory.Name}' добавлена к категории подопечного '{wardCategory.Name}'.");
            }

        }

        public bool Recognize(string command)
        {
            return command == CommandText || command == Alias;
        }
    }
}
