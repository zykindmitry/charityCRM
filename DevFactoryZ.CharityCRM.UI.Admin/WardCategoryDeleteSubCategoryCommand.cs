using DevFactoryZ.CharityCRM.Persistence;
using System;
using System.Text;
using System.Linq;
using DevFactoryZ.CharityCRM.Services;

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
        /// <param name="wardCategoryService">Экземпляр <see cref="IWardCategoryService"/> для работы с хранилищем.</param>
        public WardCategoryDeleteSubCategoryCommand(IWardCategoryService wardCategoryService)
        {
            this.wardCategoryService = wardCategoryService;
        }

        private static string CommandText = "update-ward-category-del-subcategory";

        private static string Alias = "uwcdsc";

        private static string IdWardCategoryParameter = "Id категории подопечного";

        private static string IdSubCategoryParameter = "Id подкатегории";

        public string Help =>
            (new StringBuilder($"Напишите '{CommandText} (или {Alias}) [{IdWardCategoryParameter}] [{IdSubCategoryParameter}]', чтобы удалить подкатегорию из категории подопечного. "))
            .AppendLine()
            .AppendLine($"    Внимание!!! {IdWardCategoryParameter} можно узнать, выполнив команду 'list-ward-categories' или 'lwc'.")
            .Append($"    Внимание!!! {IdSubCategoryParameter} можно узнать, выполнив команду 'list-ward-categories' или 'lwc'.")
            .ToString();
             
        private readonly IWardCategoryService wardCategoryService;

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

            var subCategory =
                wardCategoryService.GetById(subCategoryId);

            wardCategoryService.RemoveChild(wardCategoryId, subCategory);

            Console.WriteLine($"Подкатегория '{subCategory.Name}' удалена из категории подопечного '{wardCategoryId}'.");
        }

        public bool Recognize(string command)
        {
            return command == CommandText || command == Alias;
        }
    }
}
