using DevFactoryZ.CharityCRM.Persistence;
using System;
using System.Text;
using System.Linq;

namespace DevFactoryZ.CharityCRM.UI.Admin
{
    /// <summary>
    /// Имплементация <see cref="ICommand"/> для удаления подкатегории из списка подкатегорий категории подопечного БФ.
    /// </summary>
    class WardCategoryDeleteSubCategoryCommand : ICommand
    {
        /// <summary>
        /// Создвет экземпляр <see cref="WardCategoryDeleteSubCategoryCommand"/>.
        /// </summary>
        /// <param name="unitOfWorkCreator">Экземпляр <see cref="ICreateUnitOfWork"/> для работы с хранилищем.</param>
        public WardCategoryDeleteSubCategoryCommand(ICreateUnitOfWork unitOfWorkCreator)
        {
            this.unitOfWorkCreator = unitOfWorkCreator;
        }

        private static string CommandText = "update-ward-category-del-subcategory";

        private static string Alias = "uwcdsc";

        private static string IdWardCategoryParameter = "Id категории подопечного";

        private static string IdSubCategoryParameter = "Id подкатегории";

        public string Help =>
            (new StringBuilder($"Напишите '{CommandText} (или {Alias}) [{IdWardCategoryParameter}] [{IdSubCategoryParameter}]', чтобы удалить подкатегорию из категории подопечного. "))
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

                wardCategory.Deny(subCategory);
                unitOfWork.Save();

                Console.WriteLine($"Подкатегория '{subCategory.Name}' удалена из категории подопечного '{wardCategory.Name}'.");
            }

        }

        public bool Recognize(string command)
        {
            return command == CommandText || command == Alias;
        }
    }
}
