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
            var permissions = repository.GetAll();

            WriteHeader();
            permissions.Each(WriteBody);
        }

        private void WriteHeader()
        {
            Console.WriteLine($"{nameof(WardCategory.Id),10} {nameof(WardCategory.Name)}");
        }

        private void WriteBody(WardCategory permission)
        {
            Console.WriteLine($"{permission.Id,10:0} {permission.Name}");
        }

        public bool Recognize(string command)
        {
            return command == CommandText || command == Alias;
        }
    }
}
