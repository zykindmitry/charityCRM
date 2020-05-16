using DevFactoryZ.CharityCRM.Persistence;
using System;
using System.Text;
using System.Linq;

namespace DevFactoryZ.CharityCRM.UI.Admin
{
    /// <summary>
    /// Имплементация <see cref="ICommand"/> для изменения ФИО подопечного БФ в хранилище.
    /// </summary>
    class WardUpdateFullNameCommand : ICommand
    {
        /// <summary>
        /// Создвет экземпляр <see cref="WardUpdateFullNameCommand "/>.
        /// </summary>
        /// <param name="unitOfWorkCreator">Экземпляр <see cref="ICreateUnitOfWork"/> для работы с хранилищем.</param>
        public WardUpdateFullNameCommand(ICreateUnitOfWork unitOfWorkCreator)
        {
            this.unitOfWorkCreator = unitOfWorkCreator;
        }

        private static string CommandText = "update-ward-fullname";

        private static string Alias = "uwfn";

        private static string IdParameter = "Id подопечного";

        private const string FirstNameParameter = "Имя";
        private const string MiddleNameParameter = "Отчество";
        private const string SurNameParameter = "Фамилия";

        private const char valueSeparator = ' ';
        private const char keyNameSeparator = '|';
        private const string parametersSeparator = "--";
        private const int wardComponentsCount = 3;

        private static char[] FirstNameKey = new char[] { 'и', 'f' };
        private static char[] MiddleNameKey = new char[] { 'о', 'm' };
        private static char[] SurNameKey = new char[] { 'ф', 'l' };

        private static int FirstNameIndex = 0;
        private static int MiddleNameIndex = 1;
        private static int SurNameIndex = 2;

        public string Help => ComposeHelpString();
             
        private readonly ICreateUnitOfWork unitOfWorkCreator;

        public void Execute(string[] parameters)
        {
            if (!parameters.Any())
            {
                Console.WriteLine($"Ошибка! Отсутствует первый обязательный параметр: '{IdParameter}'.");
                return;
            }

            if (!int.TryParse(parameters[0], out int wardId))
            {
                Console.WriteLine($"Ошибка! Первый обязательный параметр '{IdParameter}' должен быть целым положительным числом.");
                return;
            }

            var fullName = GetOrderedOptions(parameters.Skip(1).ToArray());

            if (fullName[FirstNameIndex] == string.Empty)
            {
                Console.WriteLine(
                    $"Ошибка! Опция '{FirstNameParameter}' - не может быть пустой.");
                Console.WriteLine(
                    $"Если не требуется изменять имя подопечного, просто удалите '{parametersSeparator}{FirstNameKey.First()}' или '{parametersSeparator}{FirstNameKey.Last()}' из коммандной строки.");

                return;
            }

            if (fullName[SurNameIndex] == string.Empty)
            {
                Console.WriteLine(
                    $"Ошибка! Опция '{SurNameParameter}' - не может быть пустой.");
                Console.WriteLine(
                    $"Если не требуется изменять фамилию подопечного, просто удалите '{parametersSeparator}{SurNameKey.First()}' или '{parametersSeparator}{SurNameKey.Last()}' из коммандной строки.");

                return;
            }

            using (var unitOfWork = unitOfWorkCreator.Create())
            {
                var ward = 
                    unitOfWork.GetById<Ward, int>(wardId); 

                ward.FullName.FirstName = fullName[FirstNameIndex] ?? ward.FullName.FirstName;
                ward.FullName.MiddleName = fullName[MiddleNameIndex] ?? ward.FullName.MiddleName;
                ward.FullName.SurName = fullName[SurNameIndex] ?? ward.FullName.SurName;

                unitOfWork.Save();

                Console.WriteLine($"ФИО подопечного с идентификатором (ID = {ward.Id}) изменено.");
            }
        }

        public bool Recognize(string command)
        {
            return command == CommandText || command == Alias;
        }

        private string[] GetOrderedOptions(string[] parameters)
        {
            var source = string.Join(valueSeparator, parameters).Split(parametersSeparator);

            var ruKeys = new char[] {
                FirstNameKey.First(),
                MiddleNameKey.First(),
                SurNameKey.First(),
            };

            var enKeys = new char[] {
                FirstNameKey.Last(),
                MiddleNameKey.Last(),
                SurNameKey.Last(),
            };

            var result = new string[wardComponentsCount];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = null;
            }

            foreach (var item in source)
            {
                var index = Array.IndexOf(ruKeys, item.FirstOrDefault());

                if (index < 0)
                {
                    index = Array.IndexOf(enKeys, item.FirstOrDefault());
                }

                if (index >= 0 && index < wardComponentsCount)
                {
                    result[index] += $"{valueSeparator}{string.Concat(item.Skip(1))}".Trim();
                };
            }

            return result;
        }

        private string ComposeHelpString()
        {
            var resultString = new StringBuilder()
                .AppendLine("Изменение ФИО подопечного:")
                .Append($"  {CommandText} (кратко {Alias})")
                .Append($" <{IdParameter}>")
                .AppendLine($" {parametersSeparator}<долполнительная опция 1> <значение> {parametersSeparator}<дополнительная опция 2> <значение>... ")
                .AppendLine($"    Дополнительные опции:")
                .AppendLine($"    {parametersSeparator}{string.Join(keyNameSeparator, FirstNameKey)} - {FirstNameParameter}")
                .AppendLine($"    {parametersSeparator}{string.Join(keyNameSeparator, SurNameKey)} - {SurNameParameter}")
                .AppendLine($"    {parametersSeparator}{string.Join(keyNameSeparator, MiddleNameKey)} - {MiddleNameParameter}")
                .AppendLine($"  Внимание!!! {IdParameter} можно узнать, выполнив команду 'list-wards' или 'lw'.");

            return resultString.ToString();
        }
    }
}
