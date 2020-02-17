using DevFactoryZ.CharityCRM.Persistence;
using System;
using System.Text;
using System.Linq;

namespace DevFactoryZ.CharityCRM.UI.Admin
{
    /// <summary>
    /// Имплементация <see cref="ICommand"/> для получения конфигурации сложности пароля по его Id из хранилища.
    /// </summary>
    class PasswordConfigGetCommand : ICommand
    {
        private readonly ICreateRepository<IPasswordConfigRepository> repositoryCreator;

        /// <summary>
        /// Создвет экземпляр <see cref="PasswordConfigGetCommand"/>.
        /// </summary>
        /// <param name="repositoryCreator">Экземпляр <see cref="ICreateRepository"/> типа <see cref="IPasswordConfigRepository"/> для работы с хранилищем.</param>
        public PasswordConfigGetCommand(ICreateRepository<IPasswordConfigRepository> repositoryCreator)
        {
            this.repositoryCreator = repositoryCreator;
        }

        private static string CommandText = "get-password-config";

        private static string IdParameter = "Id конфигурации";

        public string Help =>
            (new StringBuilder($"Напишите '{CommandText} [{IdParameter}]', чтобы получить конфигурацию сложности пароля. "))
            .AppendLine()
            .Append($"    Внимание!!! {IdParameter} можно узнать, выполнив команду 'list-password-config'.")
            .ToString();        

        public void Execute(string[] parameters)
        {
            if (!parameters.Any())
            {
                Console.WriteLine($"Ошибка! Отсутствует обязательный параметр '{IdParameter}'");
                return;
            }

            if (!int.TryParse(parameters.First(), out int passwordConfigId))
            {
                Console.WriteLine($"Ошибка! Обязательный параметр '{IdParameter}' должен быть целым положительным числом.");
                return;
            }

            var repository = repositoryCreator.Create();
            var passwordConfig = repository.GetById(passwordConfigId);

            Console.WriteLine($"{nameof(PasswordConfig.Id)}: {passwordConfig.Id}");
            Console.WriteLine($"{nameof(PasswordConfig.CreatedAt)}: {passwordConfig.CreatedAt}");
            Console.WriteLine($"{nameof(PasswordConfig.MaxLifeTime)}: {passwordConfig.MaxLifeTime}");
            Console.WriteLine($"{nameof(PasswordConfig.MinLength)}: {passwordConfig.MinLength}");
            Console.WriteLine($"{nameof(PasswordConfig.SaltLength)}: {passwordConfig.SaltLength}");
            Console.WriteLine($"{nameof(PasswordConfig.UseDigits)}: {passwordConfig.UseDigits}");
            Console.WriteLine($"{nameof(PasswordConfig.UseUpperCase)}: {passwordConfig.UseUpperCase}");
            Console.WriteLine($"{nameof(PasswordConfig.UseSpecialSymbols)}: {passwordConfig.UseSpecialSymbols}");
            Console.Write($"{nameof(PasswordConfig.SpecialSymbols)}: ");
            Console.WriteLine(
                $"{ ((passwordConfig.SpecialSymbols == null || passwordConfig.SpecialSymbols.Length == 0) ? "<empty>" : string.Join("", passwordConfig.SpecialSymbols))}");
        }

        public bool Recognize(string command)
        {
            return command == CommandText;
        }
    }
}
