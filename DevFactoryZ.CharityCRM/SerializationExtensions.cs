using System;
using System.IO;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

namespace DevFactoryZ.CharityCRM
{
    /// <summary>
    /// Содержит методы расширений для сериализации/десериализации объектов.
    /// Использует <see cref="System.Runtime.Serialization"/> 
    /// </summary>
    public static class SerializationExtensions
    {
        /// <summary>
        /// Десериализует JSON-объект в виде <see cref="Stream"/> в объект указанного типа.
        /// Результирующий тип <typeparam name="T"/> должен быть помечен атрибутом [<see cref="SerializableAttribute"/>].
        /// </summary>
        /// <typeparam name="T">Результирующий тип.</typeparam>
        /// <param name="jsonObject">JSON-объект в виде <see cref="Stream"/> для десериализации.</param>
        /// <returns>Десериализованный объект типа <typeparam name="T"/></returns>
        public async static Task<T> FromJsonAsync<T>(this Stream jsonObject) where T : class
        {
            var serializer = new DataContractJsonSerializer(typeof(T));

            using (var memoryStream = new MemoryStream())
            {
                await jsonObject.CopyToAsync(memoryStream);

                memoryStream.Seek(0, SeekOrigin.Begin);

                return serializer.ReadObject(memoryStream) as T;
            }
        }

        /// <summary>
        /// Десериализует JSON-объект в виде массива <see cref="Byte"/>[] в объект указанного типа.
        /// Результирующий тип <typeparam name="T"/> должен быть помечен атрибутом [<see cref="SerializableAttribute"/>].
        /// </summary>
        /// <typeparam name="T">Результирующий тип.</typeparam>
        /// <param name="jsonObject">JSON-объект в виде массива <see cref="Byte"/>[] для десериализации.</param>
        /// <returns>Десериализованный объект типа <typeparam name="T"/></returns>
        public static T FromJson<T>(this byte[] jsonObject) where T : class
        {
            var serializer = new DataContractJsonSerializer(typeof(T));

            return serializer.ReadObject(jsonObject.ToStream()) as T;
        }

        /// <summary>
        /// Сериализует объект типа <typeparamref name="T"/> в JSON-объект
        /// и возвращает его в виде массива <see cref="Byte"/>[].
        /// Сериализуемый тип <typeparam name="T"/> должен быть помечен атрибутом [<see cref="SerializableAttribute"/>].
        /// </summary>
        /// <typeparam name="T">Тип сериализуемого объекта.</typeparam>
        /// <param name="obj">Сериализуемый объект.</param>
        /// <returns>Сериализованный в JSON объект в виде массива <see cref="Byte"/>[].</returns>
        public static byte[] ToJson<T>(this T obj)
        {
            using (var stream = new MemoryStream())
            {
                var serializer = new DataContractJsonSerializer(typeof(T));

                serializer.WriteObject(stream, obj);

                stream.Seek(0, SeekOrigin.Begin);

                return stream.ToArray();
            }
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
