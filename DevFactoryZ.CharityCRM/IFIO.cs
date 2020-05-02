using System;
using System.Collections.Generic;
using System.Text;

namespace DevFactoryZ.CharityCRM
{
    /// <summary>
    /// Интерфейс определяет компоненты имени подопечного БФ.
    /// </summary>
    public interface IFIO
    {
        /// <summary>
        /// Полное имя подопечного.
        /// </summary>
        string FullName { get; }
        
        /// <summary>
        /// Имя.
        /// </summary>
        string FirstName { get; }
        
        /// <summary>
        /// Отчество.
        /// </summary>
        string MidName { get; }
        
        /// <summary>
        /// Фамилия.
        /// </summary>
        string LastName { get; }
    }
}
