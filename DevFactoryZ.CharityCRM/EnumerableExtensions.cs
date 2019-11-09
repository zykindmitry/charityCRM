using System;
using System.Collections.Generic;

namespace DevFactoryZ.CharityCRM
{
    /// <summary>
    /// Этот класс содержит методы-расширения IEnumerable
    /// </summary>
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Выполняет действие над каждым элементом перечисления
        /// </summary>
        /// <typeparam name="T">Тип элементов перечисления</typeparam>
        /// <param name="elements">Перечисление</param>
        /// <param name="actionToDo">Действие над элементом</param>
        public static void Each<T>(this IEnumerable<T> elements, Action<T> actionToDo)
        {
            foreach(var element in elements)
            {
                actionToDo(element);
            }
        }
    }
}
