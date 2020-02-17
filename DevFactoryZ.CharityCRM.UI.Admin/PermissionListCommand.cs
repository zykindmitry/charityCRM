using DevFactoryZ.CharityCRM.Persistence;
using System;
using System.Text;
using System.Linq;

namespace DevFactoryZ.CharityCRM.UI.Admin
{
    /// <summary>
    /// Имплементация <see cref="ICommand"/> для получения списка разрешений из хранилища.
    /// </summary>
    class PermissionListCommand : ICommand
    {
        private readonly ICreateRepository<IPermissionRepository> repositoryCreator;
        /// <summary>
        /// Создвет экземпляр <see cref="PermissionListCommand"/>.
        /// </summary>
        /// <param name="repositoryCreator">Экземпляр <see cref="ICreateRepository"/> типа <see cref="IPermissionRepository"/> для работы с хранилищем.</param>
        public PermissionListCommand(ICreateRepository<IPermissionRepository> repositoryCreator)
        {
            this.repositoryCreator = repositoryCreator;
        }

        private static string CommandText = "list-permissions";

        public string Help => 
            $"Напишите '{CommandText}', чтобы получить список существующих разрешений.";

        public void Execute(string[] parameters)
        {
            var repository = repositoryCreator.Create();
            var permissions = repository.GetAll();

            WriteHeader();
            permissions.Each(WriteBody);
        }

        private void WriteHeader()
        {
            Console.WriteLine($"{nameof(Permission.Id),10} {nameof(Permission.Name)}");
        }

        private void WriteBody(Permission permission)
        {
            Console.WriteLine($"{permission.Id,10:0} {permission.Name}");
        }

        public bool Recognize(string command)
        {
            return command == CommandText;
        }
    }
}
