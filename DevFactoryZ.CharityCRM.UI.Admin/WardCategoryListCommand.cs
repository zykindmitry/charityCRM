using DevFactoryZ.CharityCRM.Persistence;
using System;
using System.Text;
using System.Linq;

namespace DevFactoryZ.CharityCRM.UI.Admin
{
    /// <summary>
    /// Имплементация <see cref="ICommand"/> для получения списка категорий подопечных из хранилища.
    /// </summary>
    class WardCategoryListCommand : ICommand
    {
        private readonly ICreateRepository<IWardCategoryRepository> repositoryCreator;
        /// <summary>
        /// Создвет экземпляр <see cref="WardCategoryListCommand"/>.
        /// </summary>
        /// <param name="repositoryCreator">Экземпляр <see cref="ICreateRepository"/> типа <see cref="IWardCategoryRepository"/> для работы с хранилищем.</param>
        public WardCategoryListCommand(ICreateRepository<IWardCategoryRepository> repositoryCreator)
        {
            this.repositoryCreator = repositoryCreator;
        }

        private static string CommandText = "list-ward-categories";

        private static string Alias = "lwc";

        public string Help => 
            $"Напишите '{CommandText} (или {Alias})', чтобы получить список существующих категорий подопечных.";

        public void Execute(string[] parameters)
        {
            var repository = repositoryCreator.Create();
            var categories = repository.GetAll();

            WriteHeader();
            categories.Each(WriteBody);
        }

        private void WriteHeader()
        {
            Console.WriteLine(
                $"{nameof(WardCategory.Id), 10}  {nameof(WardCategory.Name), -30}  {nameof(WardCategory.SubCategories), -50}");
        }

        private void WriteBody(WardCategory wardCategory)
        {
            Console.WriteLine(
                $"{wardCategory.Id, 10:0}  {wardCategory.Name, -30}  {string.Join(",", wardCategory.SubCategories.Select(s => s.WardCategory.Name)), -50}");
        }

        public bool Recognize(string command)
        {
            return command == CommandText || command == Alias;
        }
    }
}
