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
        /// <summary>
        /// Создвет экземпляр <see cref="PermissionListCommand"/>.
        /// </summary>
        /// <param name="permissionRepository">Экземпляр <see cref="IPermissionRepository"/> для работы с хранилищем.</param>
        public PermissionListCommand(IPermissionRepository permissionRepository)
        {
            this.permissionRepository = permissionRepository;
        }

        private static string CommandText = "list-permissions";

        public string Help => 
            $"Напишите '{CommandText}', чтобы получить список существующих разрешений.";

        private readonly IPermissionRepository permissionRepository;

        public void Execute(string[] parameters)
        {
            var permissions =
                permissionRepository.GetAll();

            Console.WriteLine($"{nameof(Permission.Id),10} {nameof(Permission.Name)}");

            foreach (var permission in permissions)
            {
                Console.WriteLine("{0,10:0} {1}", permission.Id, permission.Name);
            }
        }

        public bool Recognize(string command)
        {
            return command == CommandText;
        }
    }
}
