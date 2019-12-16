using DevFactoryZ.CharityCRM.Persistence;
using System;
using System.Text;
using System.Linq;

namespace DevFactoryZ.CharityCRM.UI.Admin
{
    /// <summary>
    /// Имплементация <see cref="ICommand"/> для получения списка ролей из хранилища.
    /// </summary>
    class RoleListCommand : ICommand
    {
        /// <summary>
        /// Создвет экземпляр <see cref="RoleListCommand"/>.
        /// </summary>
        /// <param name="roleRepository">Экземпляр <see cref="IRoleRepository"/> для работы с хранилищем.</param>
        public RoleListCommand(IRoleRepository roleRepository)
        {
            this.roleRepository = roleRepository;
        }

        private static string CommandText = "list-roles";

        public string Help => 
            $"Напишите '{CommandText}', чтобы получить список существующих ролей.";

        private readonly IRoleRepository roleRepository;

        public void Execute(string[] parameters)
        {
            var roles =
                roleRepository.GetAll();

            Console.WriteLine($"{nameof(Role.Id),10} {nameof(Role.Name)}");

            foreach (var role in roles)
            {
                Console.WriteLine("{0,10:0} {1}", role.Id, role.Name);
            }
        }

        public bool Recognize(string command)
        {
            return command == CommandText;
        }
    }
}
