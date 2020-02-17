using DevFactoryZ.CharityCRM.Persistence;
using System;
using System.Text;

namespace DevFactoryZ.CharityCRM.UI.Admin
{
    /// <summary>
    /// Имплементация <see cref="ICommand"/> для получения списка конфигураций сложности пароля из хранилища.
    /// </summary>
    class PasswordConfigListCommand : ICommand
    {
        private readonly ICreateRepository<IPasswordConfigRepository> repositoryCreator;

        /// <summary>
        /// Создвет экземпляр <see cref="PasswordConfigListCommand"/>.
        /// </summary>
        /// <param name="repositoryCreator">Экземпляр <see cref="ICreateRepository"/> типа <see cref="IPasswordConfigRepository"/> для работы с хранилищем.</param>
        public PasswordConfigListCommand(ICreateRepository<IPasswordConfigRepository> repositoryCreator)
        {
            this.repositoryCreator = repositoryCreator;
        }

        private static string CommandText = "list-password-config";

        public string Help => 
            $"Напишите '{CommandText}', чтобы получить список существующих конфигураций.";

        public void Execute(string[] parameters)
        {
            var repository = repositoryCreator.Create();
            var passwordConfigs = repository.GetAll();

            WriteHeader();
            passwordConfigs.Each(WriteBody);
        }

        private void WriteHeader()
        {
            var header = new StringBuilder();
            header.Append($"{nameof(PasswordConfig.Id),9}");
            header.Append($"{nameof(PasswordConfig.CreatedAt), 16}");
            header.Append($"{nameof(PasswordConfig.MaxLifeTime),17}");
            header.Append($"{nameof(PasswordConfig.MinLength),10}");
            header.Append($"{nameof(PasswordConfig.SaltLength),11}");
            header.Append($"{nameof(PasswordConfig.UseDigits),10}");
            header.Append($"{nameof(PasswordConfig.UseUpperCase),13}");
            header.Append($"{nameof(PasswordConfig.UseSpecialSymbols),18}");
            header.Append($"{nameof(PasswordConfig.SpecialSymbols),15}");

            Console.WriteLine(header.ToString());
        }

        private void WriteBody(PasswordConfig passwordConfig)
        {
            var body = new StringBuilder();
            body.Append($"{passwordConfig.Id,9:0}");
            body.Append($"{passwordConfig.CreatedAt, 21}");
            body.Append($"{passwordConfig.MaxLifeTime.Days,7:0}");
            body.Append($"{passwordConfig.MinLength,11:0}");
            body.Append($"{passwordConfig.SaltLength,11:0}");
            body.Append($"{passwordConfig.UseDigits.ToString(),12}");
            body.Append($"{passwordConfig.UseUpperCase.ToString(),12}");
            body.Append($"{passwordConfig.UseSpecialSymbols.ToString(),12}");
            body.Append(
                $"{ ((passwordConfig.SpecialSymbols == null || passwordConfig.SpecialSymbols.Length == 0) ? "<empty>" : string.Join("", passwordConfig.SpecialSymbols)), 24}");

            Console.WriteLine(body.ToString());
        }

        public bool Recognize(string command)
        {
            return command == CommandText;
        }
    }
}
