using DevFactoryZ.CharityCRM.Persistence;
using System;
using System.Text;
using System.Linq;

namespace DevFactoryZ.CharityCRM.UI.Admin
{
    /// <summary>
    /// Имплементация <see cref="ICommand"/> для изменения адрес подопечного БФ в хранилище.
    /// </summary>
    class WardUpdateAddressCommand : ICommand
    {
        /// <summary>
        /// Создвет экземпляр <see cref="WardUpdateAddressCommand "/>.
        /// </summary>
        /// <param name="unitOfWorkCreator">Экземпляр <see cref="ICreateUnitOfWork"/> для работы с хранилищем.</param>
        public WardUpdateAddressCommand(ICreateUnitOfWork unitOfWorkCreator)
        {
            this.unitOfWorkCreator = unitOfWorkCreator;
        }

        private static string CommandText = "update-ward-address";

        private static string Alias = "uwa";

        private static string IdParameter = "Id подопечного";

        private const char valueSeparator = ' ';
        private const char keyNameSeparator = '|';
        private const string parametersSeparator = "--";
        private const int addressComponentsCount = 8;

        private static string PostCodeParameter = "Почтовый индекс";
        private static string CountryParameter = "Страна";
        private static string RegionParameter = "Область, край и т.п.";
        private static string CityParameter = "Город, село и т.п.";
        private static string AreaParameter = "Район, округ и т.п.";
        private static string StreetParameter = "Улица, проспект и т.п.";
        private static string HouseParameter = "Дом, строение и т.п.";
        private static string FlatParameter = "Квартира, офис и т.п.";

        private static char[] PostCodeKey = new char[] { 'и', 'i'};
        private static char[] CountryKey = new char[] { 'с', 'n' };
        private static char[] RegionKey = new char[] { 'о', 'r' };
        private static char[] CityKey = new char[] { 'г', 'c' };
        private static char[] AreaKey = new char[] { 'р', 'a' };
        private static char[] StreetKey = new char[] { 'у', 's' };
        private static char[] HouseKey = new char[] { 'д', 'h' };
        private static char[] FlatKey = new char[] { 'к', 'o' };
        private static int PostCodeIndex = 0;
        private static int CountryIndex = 1;
        private static int RegionIndex = 2;
        private static int CityIndex = 3;
        private static int AreaIndex = 4;
        private static int StreetIndex = 5;
        private static int HouseIndex = 6;
        private static int FlatIndex = 7;

        public string Help => ComposeHelpString();
             
        private readonly ICreateUnitOfWork unitOfWorkCreator;

        public void Execute(string[] parameters)
        {
            if (!parameters.Any())
            {
                Console.WriteLine($"Ошибка! Отсутствует обязательный параметр: '{IdParameter}'.");
                return;
            }

            if (!int.TryParse(parameters[0], out int wardId))
            {
                Console.WriteLine(
                    $"Ошибка! Первый обязательный параметр '{IdParameter}' должен быть целым положительным числом.");

                return;
            }

            var address = GetOrderedAddress(parameters.Skip(1).ToArray());
            
            if ( address[CityIndex] == string.Empty)
            {
                Console.WriteLine(
                    $"Ошибка! Обязательная опция '{CityParameter}' - должна содержать название города подопечного.");
                Console.WriteLine(
                    $"Если не требуется изменять название города, просто удалите '{parametersSeparator}{CityKey.First()}' или '{parametersSeparator}{CityKey.Last()}' из коммандной строки.");

                return;
            }

            using (var unitOfWork = unitOfWorkCreator.Create())
            {
                var ward = 
                    unitOfWork.GetById<Ward, int>(wardId);

                ward.Address.PostCode = address[PostCodeIndex] ?? ward.Address.PostCode;
                ward.Address.Country = address[CountryIndex] ?? ward.Address.Country;
                ward.Address.Region = address[RegionIndex] ?? ward.Address.Region;
                ward.Address.City = address[CityIndex] ?? ward.Address.City;
                ward.Address.Area = address[AreaIndex] ?? ward.Address.Area;
                ward.Address.Street = address[StreetIndex] ?? ward.Address.Street;
                ward.Address.House = address[HouseIndex] ?? ward.Address.House;
                ward.Address.Flat = address[FlatIndex] ?? ward.Address.Flat;

                unitOfWork.Save();

                Console.WriteLine(
                    $"Адрес подопечного с идентификатором (ID = {ward.Id}) изменен.");
            }
        }

        public bool Recognize(string command)
        {
            return command == CommandText || command == Alias;
        }

        private string[] GetOrderedAddress(string[] parameters)
        {
            var source = string.Join(valueSeparator, parameters).Split(parametersSeparator);

            var ruKeys = new char[] {
                PostCodeKey.First(),
                CountryKey.First(),
                RegionKey.First(),
                CityKey.First(),
                AreaKey.First(),
                StreetKey.First(),
                HouseKey.First(),
                FlatKey.First()
            };

            var enKeys = new char[] {
                PostCodeKey.Last(),
                CountryKey.Last(),
                RegionKey.Last(),
                CityKey.Last(),
                AreaKey.Last(),
                StreetKey.Last(),
                HouseKey.Last(),
                FlatKey.Last()
            };

            var result = new string[addressComponentsCount];
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

                if (index >= 0 && index < addressComponentsCount)
                {
                    result[index] += $"{valueSeparator}{string.Concat(item.Skip(1))}".Trim();
                };
            }

            return result;
        }

        private string ComposeHelpString()
        {
            var resultString = new StringBuilder()
                .AppendLine("Изменение адреса подопечного:")
                .Append($"  {CommandText} (кратко {Alias})")
                .Append($" <{IdParameter}>")
                .Append($" {parametersSeparator}<обязательная опция 1> <значение> {parametersSeparator}<обязательная опция 2> <значение>... ")
                .AppendLine($" {parametersSeparator}<долполнительная опция 1> <значение> {parametersSeparator}<дополнительная опция 2> <значение>... ")
                .AppendLine($"    Обязательные опции:")
                .AppendLine($"    {parametersSeparator}{string.Join(keyNameSeparator, CityKey)} - {CityParameter}")
                .AppendLine($"    Дополнительные опции:")
                .AppendLine($"    {parametersSeparator}{string.Join(keyNameSeparator, PostCodeKey)} - {PostCodeParameter}")
                .AppendLine($"    {parametersSeparator}{string.Join(keyNameSeparator, CountryKey)} - {CountryParameter}")
                .AppendLine($"    {parametersSeparator}{string.Join(keyNameSeparator, RegionKey)} - {RegionParameter}")
                .AppendLine($"    {parametersSeparator}{string.Join(keyNameSeparator, AreaKey)} - {AreaParameter}")
                .AppendLine($"    {parametersSeparator}{string.Join(keyNameSeparator, StreetKey)} - {StreetParameter}")
                .AppendLine($"    {parametersSeparator}{string.Join(keyNameSeparator, HouseKey)} - {HouseParameter}")
                .AppendLine($"    {parametersSeparator}{string.Join(keyNameSeparator, FlatKey)} - {FlatParameter}")
                .AppendLine($"  Внимание!!! {IdParameter} можно узнать, выполнив команду 'list-wards' или 'lw'.");
            
            return resultString.ToString();
        }
    }
}
