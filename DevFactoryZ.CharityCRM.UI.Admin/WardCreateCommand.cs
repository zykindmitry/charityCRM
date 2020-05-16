using DevFactoryZ.CharityCRM.Persistence;
using System;
using System.Linq;
using System.Text;

namespace DevFactoryZ.CharityCRM.UI.Admin
{
    /// <summary>
    /// Имплементация <see cref="ICommand"/> для сохранения нового подопечного БФ в хранилище.
    /// </summary>
    class WardCreateCommand : ICommand
    {
        //private const char valueSeparator = ',';

        /// <summary>
        /// Создает экземпляр <see cref="WardCreateCommand"/>
        /// </summary>
        /// <param name="unitOfWorkCreator">Экземпляр <see cref="ICreateUnitOfWork"/> для работы с хранилищем.</param>
        public WardCreateCommand(ICreateUnitOfWork unitOfWorkCreator)
        {
            this.unitOfWorkCreator = unitOfWorkCreator;
        }

        private const string CommandText = "create-ward";

        private static string Alias = "cw";

        private const string FirstNameParameter = "Имя";
        private const string MiddleNameParameter = "Отчество";
        private const string SurNameParameter = "Фамилия";
        private const string BirthDateParameter = "Дата рождения";
        private const string PhoneParameter = "Номер телефона";
        private static string CityParameter = "Город, село и т.п.";

        private const char valueSeparator = ' ';
        private const char keyNameSeparator = '|';
        private const string parametersSeparator = "--";
        private const int wardComponentsCount = 6;

        private static char[] FirstNameKey = new char[] { 'и', 'f' };
        private static char[] MiddleNameKey = new char[] { 'о', 'm' };
        private static char[] SurNameKey = new char[] { 'ф', 'l' };
        private static char[] BirthDateKey = new char[] { 'р', 'b' };
        private static char[] PhoneKey = new char[] { 'т', 'p' };
        private static char[] CityKey = new char[] { 'г', 'c' };

        private static int FirstNameIndex = 0;
        private static int MiddleNameIndex = 1;
        private static int SurNameIndex = 2;
        private static int BirthDateIndex = 3;
        private static int PhoneIndex = 4;
        private static int CityIndex = 5;


        public string Help => ComposeHelpString();

        private readonly ICreateUnitOfWork unitOfWorkCreator;

        public void Execute(string[] parameters)
        {

            if (!parameters.Any() || !parameters.Any(p => p.Contains(parametersSeparator)))
            {
                Console.WriteLine
                    ($"Ошибка! Отсутствует несколько обязательных опций: '{FirstNameParameter}', '{SurNameParameter}', '{BirthDateParameter}', '{CityParameter}'.");

                return;
            }

            if (!parameters.Any(p => p.Contains($"{parametersSeparator}{BirthDateKey.First()}"))
                && !parameters.Any(p => p.Contains($"{parametersSeparator}{BirthDateKey.Last()}")))
            {
                Console.WriteLine
                    ($"Ошибка! Отсутствует обязательная опция: '{BirthDateParameter}'");

                return;
            }
            
            var orderedWardComponent = GetOrderedOptions(parameters);

            if (!DateTime.TryParse(orderedWardComponent[BirthDateIndex], out DateTime birthDate))
            {
                Console.WriteLine($"Ошибка! Неправильный формат даты в обязательной опции '{BirthDateParameter}'");
                return;
            }

            using (var unitOfWork = unitOfWorkCreator.Create())
            {
                var wardRegistration =
                    new Ward(
                        new FullName(
                            orderedWardComponent[SurNameIndex],
                            orderedWardComponent[FirstNameIndex],
                            orderedWardComponent[MiddleNameIndex]),
                        new Address(
                            string.Empty, 
                            string.Empty, 
                            string.Empty, 
                            orderedWardComponent[CityIndex], 
                            string.Empty, 
                            string.Empty, 
                            string.Empty, 
                            string.Empty),
                        birthDate,
                        orderedWardComponent[PhoneIndex] ?? string.Empty,
                        Array.Empty<WardCategory>());

                unitOfWork.Add(wardRegistration);
                unitOfWork.Save();

                Console.WriteLine(
                    $"Подопечный '{wardRegistration.FullName}', {wardRegistration.BirthDate.ToShortDateString()} г.р., создан с идентификатором (ID = {wardRegistration.Id})");
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
                BirthDateKey.First(),
                PhoneKey.First(),
                CityKey.First()
            };

            var enKeys = new char[] {
                FirstNameKey.Last(),
                MiddleNameKey.Last(),
                SurNameKey.Last(),
                BirthDateKey.Last(),
                PhoneKey.Last(),
                CityKey.Last()
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
                .AppendLine("Cоздание подопечного:")
                .Append($"  {CommandText} (кратко {Alias})")
                .Append($" {parametersSeparator}<обязательная опция 1> <значение> {parametersSeparator}<обязательная опция 2> <значение>... ")
                .AppendLine($" {parametersSeparator}<долполнительная опция 1> <значение> {parametersSeparator}<дополнительная опция 2> <значение>... ")
                .AppendLine($"    Обязательные опции:")
                .AppendLine($"    {parametersSeparator}{string.Join(keyNameSeparator, FirstNameKey)} - {FirstNameParameter}")
                .AppendLine($"    {parametersSeparator}{string.Join(keyNameSeparator, SurNameKey)} - {SurNameParameter}")
                .AppendLine($"    {parametersSeparator}{string.Join(keyNameSeparator, BirthDateKey)} - {BirthDateParameter}")
                .AppendLine($"    {parametersSeparator}{string.Join(keyNameSeparator, CityKey)} - {CityParameter}")
                .AppendLine($"    Дополнительные опции:")
                .AppendLine($"    {parametersSeparator}{string.Join(keyNameSeparator, MiddleNameKey)} - {MiddleNameParameter}")
                .AppendLine($"    {parametersSeparator}{string.Join(keyNameSeparator, PhoneKey)} - {PhoneParameter}");

            return resultString.ToString();
        }
    }
}
