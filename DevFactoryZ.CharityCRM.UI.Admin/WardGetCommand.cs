using DevFactoryZ.CharityCRM.Persistence;
using System;
using System.Text;
using System.Linq;

namespace DevFactoryZ.CharityCRM.UI.Admin
{
    /// <summary>
    /// Имплементация <see cref="ICommand"/> для получения подопечного БФ по еге Id из хранилища.
    /// </summary>
    class WardGetCommand : ICommand
    {
        /// <summary>
        /// Создвет экземпляр <see cref="WardGetCommand"/>.
        /// </summary>
        /// <param name="repositoryCreator">Экземпляр <see cref="IWardRepository"/> для работы с хранилищем.</param>
        public WardGetCommand(ICreateRepository<IWardRepository> repositoryCreator)
        {
            this.repositoryCreator = repositoryCreator;
        }

        private static string CommandText = "get-ward";

        private static string Alias = "gw";

        private static string IdParameter = "Id подопечного";

        public string Help =>
            (new StringBuilder($"Напишите '{CommandText} (или {Alias}) [{IdParameter}]', чтобы получить подопечного. "))
            .AppendLine()
            .Append($"    Внимание!!! {IdParameter} можно узнать, выполнив команду 'list-wards'.")
            .ToString();

        private readonly ICreateRepository<IWardRepository> repositoryCreator;

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

            var repository = repositoryCreator.Create();
            var ward = repository.GetById(roleId);

            Console.WriteLine($"{nameof(Ward.FIO)}: {ward.FIO.FullName}.");
            Console.WriteLine($"{nameof(Ward.BirthDate)}: {ward.BirthDate.ToShortDateString()}.");
            Console.WriteLine($"{nameof(Ward.Address)}: {(string.IsNullOrWhiteSpace(ward.Address.FullAddress) ? "<empty>" : ward.Address.FullAddress)}.");
            Console.WriteLine($"{nameof(Ward.Phone)}: {(string.IsNullOrWhiteSpace(ward.Phone) ? "<empty>" : ward.Phone)}.");
            Console.WriteLine($"{nameof(Ward.CreatedAt)}: {ward.BirthDate}.");
            Console.WriteLine("Категории:");

            if (ward.WardCategories.Count() > 0)
            {
                Console.WriteLine($"{nameof(WardCategory.Id),10} {nameof(WardCategory.Name)}");

                foreach (var wardCategory in ward.WardCategories)
                {
                    Console.WriteLine("{0,10:0} {1}", wardCategory.WardCategory.Id, wardCategory.WardCategory.Name);
                }
            }
            else
            {
                Console.WriteLine($"{"<empty>", 15}");
            }
        }

        public bool Recognize(string command)
        {
            return command == CommandText || command == Alias;
        }
    }
}
