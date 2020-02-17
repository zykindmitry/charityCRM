using DevFactoryZ.CharityCRM.Persistence;
using System;
using System.Text;

namespace DevFactoryZ.CharityCRM.UI.Admin
{
    /// <summary>
    /// Имплементация <see cref="ICommand"/> для сохранения новой конфигурации сложности пароля в хранилище.
    /// </summary>
    class PasswordConfigCreateCommand : ICommand
    {
        /// <summary>
        /// Создает экземпляр <see cref="PasswordConfigCreateCommand"/>
        /// </summary>
        /// <param name="unitOfWorkCreator">Экземпляр <see cref="ICreateUnitOfWork"/> для работы с хранилищем.</param>
        public PasswordConfigCreateCommand(ICreateUnitOfWork unitOfWorkCreator)
        {
            this.unitOfWorkCreator = unitOfWorkCreator;
        }

        private const string CommandText = "create-password-config";

        private const string MaxLifeTimeParameter = "Срок действия пароля (дни)";

        private const string MinLengthParameter = "Минимальная длина пароля";

        private const string SaltLengthParameter = "Длина 'соли' (синхропосылки)";

        private const string UseDigitsParameter = "Флаг обязательного использования цифр для усложнения пароля";

        private const string UseUpperCaseParameter = "Флаг обязательного использования букв в верхнем регистре для усложнения пароля";

        private const string UseSpecialSymbolsParameter = "Флаг обязательного использования дополнительных (специальных) символов для усложнения пароля";

        private const string SpecialSymbolsParameter = "Массив дополнительных символов, которые могут использоваться для усложнения пароля";

        private const int MaxLifeTime = 0;
        
        private const int MinLength = 1;
        
        private const int SaltLength = 2;

        private const int UseDigits = 3;

        private const int UseUpperCase = 4;

        private const int UseSpecialSymbols = 5;

        private const int SpecialSymbols = 6;

        public string Help => ComposeHelpString();

        private readonly ICreateUnitOfWork unitOfWorkCreator;

        public void Execute(string[] parameters)
        {
            using(var unitOfWork = unitOfWorkCreator.Create())
            {
                var passwordConfig = parameters.Length == 0
                    ? new PasswordConfig()
                    : (bool.TryParse(parameters[UseSpecialSymbols], out bool result) && result == true
                        ? (parameters.Length == 7
                            ? (new PasswordConfig(TimeSpan.FromDays(double.Parse(parameters[MaxLifeTime]))
                                , int.Parse(parameters[MinLength])
                                , int.Parse(parameters[SaltLength])
                                , bool.Parse(parameters[UseDigits])
                                , bool.Parse(parameters[UseUpperCase])
                                , result
                                , parameters[SpecialSymbols].ToCharArray()))
                            : throw new ArgumentException($"Требуется параметр '{SpecialSymbolsParameter}'."
                                , $"{nameof(parameters)}[{SpecialSymbols}]"))
                        : (new PasswordConfig(TimeSpan.FromDays(double.Parse(parameters[MaxLifeTime]))
                                , int.Parse(parameters[MinLength])
                                , int.Parse(parameters[SaltLength])
                                , bool.Parse(parameters[UseDigits])
                                , bool.Parse(parameters[UseUpperCase])
                                , false
                                , Array.Empty<char>())));

                unitOfWork.Add(passwordConfig);
                unitOfWork.Save();

                Console.WriteLine($"Конфигурвция сложности пароля создана с идентификатором (ID = {passwordConfig.Id})");
            }
        }

        public bool Recognize(string command)
        {
            return command == CommandText;
        }

        private string ComposeHelpString()
        {
            var samplePasswordConfig = new PasswordConfig();

            var resultString = new StringBuilder();
            resultString.AppendLine($"Напишите:");
            resultString.Append($" '{CommandText}");
            resultString.Append($" '[{MaxLifeTimeParameter}]'");
            resultString.Append($" '[{MinLengthParameter}]'");
            resultString.Append($" '[{SaltLengthParameter}]'");
            resultString.Append($" '[{UseDigitsParameter}]'");
            resultString.Append($" '[{UseUpperCaseParameter}]'");
            resultString.Append($" '[{UseSpecialSymbolsParameter}'");
            resultString.AppendLine($" '[{SpecialSymbolsParameter}]]'");
            resultString.AppendLine(", чтобы создать конфигурацию сложности пароля.");
            resultString.AppendLine($"Если параметр '{UseSpecialSymbolsParameter}' == false, то параметр '{SpecialSymbolsParameter}' необязателен.") ;
            resultString.AppendLine(" При отсутствии параметров создастся конфигурация по умолчанию:");
            resultString.AppendLine($"{nameof(PasswordConfig.MaxLifeTime)} = {samplePasswordConfig.MaxLifeTime.TotalDays} дней;");
            resultString.AppendLine($"{nameof(PasswordConfig.MinLength)} = {samplePasswordConfig.MinLength} символов;");
            resultString.AppendLine($"{nameof(PasswordConfig.SaltLength)} = {samplePasswordConfig.SaltLength} символов;");
            resultString.AppendLine($"{nameof(PasswordConfig.UseDigits)} = {samplePasswordConfig.UseDigits.ToString()};");
            resultString.AppendLine($"{nameof(PasswordConfig.UseUpperCase)} = {samplePasswordConfig.UseUpperCase.ToString()};");
            resultString.AppendLine($"{nameof(PasswordConfig.UseSpecialSymbols)} = {samplePasswordConfig.UseSpecialSymbols.ToString()};");
            resultString.AppendLine($"{nameof(PasswordConfig.SpecialSymbols)} = {string.Join(' ', samplePasswordConfig.SpecialSymbols)}");

            return resultString.ToString();
        }
    }
}
