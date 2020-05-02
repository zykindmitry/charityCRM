using DevFactoryZ.CharityCRM.Persistence;
using System;
using System.Text;
using System.Linq;

namespace DevFactoryZ.CharityCRM.UI.Admin
{
    /// <summary>
    /// Имплементация <see cref="ICommand"/> для получения категории подопечного по его Id из хранилища.
    /// </summary>
    class WardCategoryGetCommand : ICommand
    {
        private readonly ICreateRepository<IWardCategoryRepository> repositoryCreator;
        /// <summary>
        /// Создвет экземпляр <see cref="WardCategoryGetCommand"/>.
        /// </summary>
        /// <param name="repositoryCreator">Экземпляр <see cref="ICreateRepository"/> типа <see cref="IWardCategoryRepository"/> для работы с хранилищем.</param>
        public WardCategoryGetCommand(ICreateRepository<IWardCategoryRepository> repositoryCreator)
        {
            this.repositoryCreator = repositoryCreator;
        }

        private static string CommandText = "get-ward-category";

        private static string Alias = "gwc";

        private static string IdParameter = "Id категории подопечного";

        public string Help =>
            (new StringBuilder($"Напишите '{CommandText} (или {Alias}) [{IdParameter}]', чтобы получить категорию подопечного. "))
            .AppendLine()
            .Append($"    Внимание!!! {IdParameter} можно узнать, выполнив команду 'list-ward-categories'.")
            .ToString();        

        public void Execute(string[] parameters)
        {
            if (!parameters.Any())
            {
                Console.WriteLine($"Ошибка! Отсутствует обязательный параметр '{IdParameter}'");
                return;
            }

            if (!int.TryParse(parameters.First(), out int wardCategoryId))
            {
                Console.WriteLine($"Ошибка! Обязательный параметр '{IdParameter}' должен быть целым положительным числом.");
                return;
            }

            var repository = repositoryCreator.Create();
            var wardCategory = repository.GetById(wardCategoryId);

            Console.WriteLine($"{nameof(WardCategory.Name)}: {wardCategory.Name}.");
            Console.WriteLine("Подкаегории:");

            if (wardCategory.SubCategories.Count() > 0)
            {
                Console.WriteLine($"{nameof(WardCategory.Id),10} {nameof(WardCategory.Name)}");

                foreach (var subCategory in wardCategory.SubCategories)
                {
                    Console.WriteLine("{0,10:0} {1}", subCategory.Id, subCategory.Name);
                }
            }
            else
            {
                Console.WriteLine($"{"<empty>",15}");
            }
        }

        public bool Recognize(string command)
        {
            return command == CommandText || command == Alias;
        }
    }
}
