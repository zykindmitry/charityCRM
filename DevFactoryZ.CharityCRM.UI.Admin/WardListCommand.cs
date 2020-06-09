using DevFactoryZ.CharityCRM.Persistence;
using System;
using System.Linq;
using System.Text;

namespace DevFactoryZ.CharityCRM.UI.Admin
{
    /// <summary>
    /// Имплементация <see cref="ICommand"/> для получения списка подопечных БФ из хранилища.
    /// </summary>
    class WardListCommand : ICommand
    {
        private readonly ICreateRepository<IWardRepository> repositoryCreator;

        /// <summary>
        /// Создвет экземпляр <see cref="WardListCommand"/>.
        /// </summary>
        /// <param name="repositoryCreator">Экземпляр <see cref="ICreateRepository"/> типа <see cref="IWardRepository"/> для работы с хранилищем.</param>
        public WardListCommand(ICreateRepository<IWardRepository> repositoryCreator)
        {
            this.repositoryCreator = repositoryCreator;
        }

        private static string CommandText = "list-wards";

        private static string Alias = "lw";

        public string Help => 
            $"Напишите '{CommandText} (или {Alias})', чтобы получить список существующих .";

        public void Execute(string[] parameters)
        {
            var repository = repositoryCreator.Create();
            var wards = repository.GetAll();

            WriteHeader();
            wards.Each(WriteBody);
        }

        private void WriteHeader()
        {
            var header = new StringBuilder()
                .Append($"{nameof(Ward.Id), 9}")
                .Append($"  {nameof(Ward.FullName), -50}")
                .Append($"  {nameof(Ward.BirthDate), -10}")
                .Append($"  {nameof(Ward.Phone), -17}")
                .Append($"  {nameof(Ward.CreatedAt) + " (UTC)", -19}")
                .Append($"  {nameof(Ward.WardCategories), -30}");

            Console.WriteLine(header.ToString());
        }

        private void WriteBody(Ward ward)
        {
            var body = new StringBuilder()
                .Append($"{ward.Id,9:0}")
                .Append($"  {ward.FullName,-50}")
                .Append($"  {ward.BirthDate.ToShortDateString(),-10:dd.MM.yyyy}")
                .Append(long.TryParse(ward.Phone, out long phoneNumber)
                    ? $"  { phoneNumber,-17:+##(###)###-##-##}"
                    : $"  {"    <empty>",-17}")
                .Append($"  {ward.CreatedAt,-19:dd.MM.yyyy HH:mm:ss}")
                .Append($"  {string.Join(", ", ward.WardCategories.Select(c => c.WardCategory.Name)), -30}");

            Console.WriteLine(body.ToString());
        }

        public bool Recognize(string command)
        {
            return command == CommandText || command == Alias;
        }
    }
}
