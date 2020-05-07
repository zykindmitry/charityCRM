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

        private static char[] PostCodeKey = new char[] { 'и', 'i'};
        private static char[] CountryKey = new char[] { 'с', 'c' };
        private static char[] RegionKey = new char[] { 'о', 'r' };
        private static char[] CityKey = new char[] { 'г', 't' };
        private static char[] AreaKey = new char[] { 'р', 'a' };
        private static char[] StreetKey = new char[] { 'у', 's' };
        private static char[] HouseKey = new char[] { 'д', 'h' };
        private static char[] FlatKey = new char[] { 'к', 'f' };
        private static int PostCodeIndex = 0;
        private static int CountryIndex = 1;
        private static int RegionIndex = 2;
        private static int CityIndex = 3;
        private static int AreaIndex = 4;
        private static int StreetIndex = 5;
        private static int HouseIndex = 6;
        private static int FlatIndex = 7;

        private static string NewAddressParameter = "Адрес";

        public string Help =>
            (new StringBuilder($"Напишите '{CommandText} (или {Alias}) [{IdParameter}] "))
            .Append($"[({parametersSeparator}{string.Join(keyNameSeparator, PostCodeKey)} <Почтовый индекс>) ")
            .Append($"({parametersSeparator}{string.Join(keyNameSeparator, CountryKey)} <Страна>) ")
            .Append($"({parametersSeparator}{string.Join(keyNameSeparator, RegionKey)} <Область, край и т.п.>) ")
            .Append($"({parametersSeparator}{string.Join(keyNameSeparator, CityKey)} <Город, село и т.п.>) ")
            .Append($"({parametersSeparator}{string.Join(keyNameSeparator, AreaKey)} <Район>) ")
            .Append($"({parametersSeparator}{string.Join(keyNameSeparator, StreetKey)} <Улица>) ")
            .Append($"({parametersSeparator}{string.Join(keyNameSeparator, HouseKey)} <Дом>) ")
            .Append($"({parametersSeparator}{string.Join(keyNameSeparator, FlatKey)} <Квартира>)]'")
            .AppendLine(", чтобы изменить адрес подопечного. ")
            .Append($"    Внимание!!! {IdParameter} можно узнать, выполнив команду 'list-wards' или 'lw'.")
            .ToString();
             
        private readonly ICreateUnitOfWork unitOfWorkCreator;

        public void Execute(string[] parameters)
        {
            if (parameters.Length < 2)
            {
                Console.WriteLine($"Ошибка! Отсутствует один или несколько обязательных параметра: '{IdParameter}', '{NewAddressParameter}'");
                return;
            }

            if (!int.TryParse(parameters[0], out int wardId))
            {
                Console.WriteLine($"Ошибка! Первый обязательный параметр '{IdParameter}' должен быть целым положительным числом.");
                return;
            }

            var address = parameters.Skip(1).ToArray();
            address = GetOrderedAddress( string.Join(valueSeparator, address).Split(parametersSeparator));
            
            if (address.Length == 0)
            {
                Console.WriteLine($"Ошибка! Второй обязательный параметр '{NewAddressParameter}' - должен содержать хотя бы одно значение.");
                return;
            }

            using (var unitOfWork = unitOfWorkCreator.Create())
            {
                var ward = 
                    unitOfWork.GetById<Ward, int>(wardId);
               
                ward.Address.Update(new Address(
                    address[PostCodeIndex]
                    , address[CountryIndex]
                    , address[RegionIndex]
                    , address[CityIndex]
                    , address[AreaIndex]
                    , address[StreetIndex]
                    , address[HouseIndex]
                    , address[FlatIndex]));

                unitOfWork.Save();

                Console.WriteLine($"Адрес подопечного с идентификатором (ID = {ward.Id}) изменен.");
            }
        }

        public bool Recognize(string command)
        {
            return command == CommandText || command == Alias;
        }

        private string[] GetOrderedAddress(string[] source)
        {
            var ruKey = new char[] { PostCodeKey.First(), CountryKey.First(), RegionKey.First(), CityKey.First(), AreaKey.First(), StreetKey.First(), HouseKey.First(), FlatKey.First() };
            var enKey = new char[] { PostCodeKey.Last(), CountryKey.Last(), RegionKey.Last(), CityKey.Last(), AreaKey.Last(), StreetKey.Last(), HouseKey.Last(), FlatKey.Last() };

            var result = new string[addressComponentsCount];
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = string.Empty;
            }

            foreach (var item in source)
            {
                var index = Array.IndexOf(ruKey, item.FirstOrDefault());

                if (index < 0)
                {
                    index = Array.IndexOf(enKey, item.FirstOrDefault());
                }

                if (index > 0 && index < addressComponentsCount) 
                {
                    result[index] += $"{valueSeparator}{string.Concat(item.Skip(1)).Trim()}";
                };
            }

            return result;
        }


    }
}
