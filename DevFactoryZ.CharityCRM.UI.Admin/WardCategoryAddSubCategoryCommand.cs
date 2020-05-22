using System;
using System.Text;
using DevFactoryZ.CharityCRM.Services;

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
        /// <param name="wardCategoryService">Экземпляр <see cref="IWardCategoryService"/> для работы с хранилищем.</param>
        public WardCategoryAddSubCategoryCommand(IWardCategoryService wardCategoryService)
        {
            this.wardCategoryService = wardCategoryService;
        }

        private static string CommandText = "update-ward-category-add-subcategory";

        private static string Alias = "uwcasc";

        private static string IdWardCategoryParameter = "Id категории подопечного";

        private static string IdSubCategoryParameter = "Id подкатегории";

        public string Help =>
            (new StringBuilder($"Напишите '{CommandText} (или {Alias}) [{IdWardCategoryParameter}] [{IdSubCategoryParameter}]', чтобы добавить подкатегорию для категории подопечного. "))
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

            wardCategoryService.AddChild(wardCategoryId, subCategoryId);

            Console.WriteLine($"Подкатегория '{subCategoryId}' добавлена к категории подопечного '{wardCategoryId}'.");

        }

        public bool Recognize(string command)
        {
            return command == CommandText || command == Alias;
        }
    }
}
