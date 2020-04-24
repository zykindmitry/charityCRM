using System;
using System.Text;
using System.IO;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DevFactoryZ.CharityCRM
{
    /// <summary>
    /// Содержит методы расширений для сериализации/десериализации объектов.
    /// Использует <see cref="Newtonsoft.Json"/> 
    /// </summary>
    public static class SerializationExtensions
    {
        /// <summary>
        /// Десериализует JSON-объект в виде <see cref="Stream"/> в объект указанного типа.
        /// </summary>
        /// <typeparam name="T">Результирующий тип.</typeparam>
        /// <param name="jsonObject">JSON-объект в виде <see cref="Stream"/> для десериализации.</param>
        /// <returns>Десериализованный объект типа <typeparam name="T"/></returns>
        public async static Task<T> FromJsonAsync<T>(this Stream jsonObject) where T : class
        {
            using (var memoryStream = new MemoryStream())
            {
                await jsonObject.CopyToAsync(memoryStream);

                memoryStream.Seek(0, SeekOrigin.Begin);

                return await Desearialize<T>(memoryStream);
            }
        }

        /// <summary>
        /// Десериализует JSON-объект в виде массива <see cref="Byte"/>[] в объект указанного типа.
        /// </summary>
        /// <typeparam name="T">Результирующий тип.</typeparam>
        /// <param name="jsonObject">JSON-объект в виде массива <see cref="Byte"/>[] для десериализации.</param>
        /// <returns>Десериализованный объект типа <typeparam name="T"/></returns>
        public async static Task<T> FromJsonAsync<T>(this byte[] jsonObject) where T : class
        {
            return await Desearialize<T>(jsonObject.ToStream());
        }

        private async static Task<T> Desearialize<T>(Stream stream)
        {
            using (var streamReader = new StreamReader(stream))
            {
                return JsonConvert.DeserializeObject<T>(await streamReader.ReadToEndAsync());
            }
        }

        /// <summary>
        /// Сериализует объект типа <typeparamref name="T"/> в JSON-объект
        /// и возвращает его в виде массива <see cref="Byte"/>[] в кодировке UTF8.
        /// </summary>
        /// <typeparam name="T">Тип сериализуемого объекта.</typeparam>
        /// <param name="obj">Сериализуемый объект.</param>
        /// <returns>Сериализованный в JSON объект в виде массива <see cref="Byte"/>[].</returns>
        public static byte[] ToJson<T>(this T obj)
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(obj, typeof(T), null));
        }

        /// <summary>
        /// Сериализует объект типа <typeparamref name="T"/> в JSON-объект
        /// и возвращает его в виде строки <see cref="string"/>.
        /// </summary>
        /// <typeparam name="T">Тип сериализуемого объекта.</typeparam>
        /// <param name="obj">Сериализуемый объект.</param>
        /// <returns>Сериализованный в JSON объект в виде строки <see cref="string"/>.</returns>
        public static string ToJsonString<T>(this T obj)
        {
            return JsonConvert.SerializeObject(obj, typeof(T), null);
        }

        /// <summary>
        /// Преобразует массив <see cref="Byte"/>[] в <see cref="Stream"/>.
        /// </summary>
        /// <param name="obj">Исходный массив <see cref="Byte"/>[].</param>
        /// <returns>Результирующий <see cref="Stream"/>.</returns>
        public static Stream ToStream(this byte[] obj)
        {
                var stream = new MemoryStream(obj);

                stream.Seek(0, SeekOrigin.Begin);

                return stream;
        }       
    }
}
