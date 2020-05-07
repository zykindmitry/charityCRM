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
            var header = new StringBuilder();
            header.Append($"{nameof(Ward.Id), 9}");
            header.Append($"  {nameof(Ward.FullName),-50}");
            header.Append($"  {nameof(Ward.BirthDate), -10}");
            header.Append($"  {nameof(Ward.Phone), -16}");
            header.Append($"  {nameof(Ward.CreatedAt) + " (UTC)",-19}");

            Console.WriteLine(header.ToString());
        }

        private void WriteBody(Ward passwordConfig)
        {
            var body = new StringBuilder();
            body.Append($"{passwordConfig.Id, 9:0}");
            body.Append($"  {passwordConfig.FullName,-50}");
            body.Append($"  {passwordConfig.BirthDate.ToShortDateString(), -10:dd.MM.yyyy}");
            body.Append(long.TryParse(passwordConfig.Phone, out long phoneNumber) 
                ? $"  { phoneNumber, -16:+#(###)###-##-##}" 
                : $"  {string.Empty, -16}");
            body.Append($"  {passwordConfig.CreatedAt,-19:dd.MM.yyyy HH:mm:ss}");

            Console.WriteLine(body.ToString());
        }

        public bool Recognize(string command)
        {
            return command == CommandText || command == Alias;
        }
    }
}
