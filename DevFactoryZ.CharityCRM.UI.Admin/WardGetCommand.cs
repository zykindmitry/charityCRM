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
            .Append($"    Внимание!!! {IdParameter} можно узнать, выполнив команду 'list-wards' или 'lw'.")
            .ToString();

        private readonly ICreateRepository<IWardRepository> repositoryCreator;

        public void Execute(string[] parameters)
        {
            if (!parameters.Any())
            {
                Console.WriteLine($"Ошибка! Отсутствует обязательный параметр '{IdParameter}'");
                return;
            }

            if (!int.TryParse(parameters.First(), out int wardId))
            {
                Console.WriteLine($"Ошибка! Обязательный параметр '{IdParameter}' должен быть целым положительным числом.");
                return;
            }

            var repository = repositoryCreator.Create();
            var ward = repository.GetById(wardId);

            var phone = long.TryParse(ward.Phone, out long phoneNumber)
                ? $"{ phoneNumber:+##(###)###-##-##}"
                : "<empty>";
            
            var address = string.IsNullOrWhiteSpace(ward.Address.ToString().Replace(',', ' ').Trim()) 
                ? "<empty>" 
                : ward.Address.ToString();

            Console.WriteLine($"{nameof(Ward.FullName), -10}: {ward.FullName}.");
            Console.WriteLine($"{nameof(Ward.BirthDate), -10}: {ward.BirthDate.ToShortDateString()}.");
            Console.WriteLine($"{nameof(Ward.Address), -10}: {address}.");
            Console.WriteLine($"{nameof(Ward.Phone), -10}: {phone}.");
            Console.WriteLine($"{nameof(Ward.CreatedAt), -10}: {ward.CreatedAt:dd.MM.yyyy HH:mm:ss}.");
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
