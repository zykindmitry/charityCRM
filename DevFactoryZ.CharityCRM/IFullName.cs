using System;
using System.Collections.Generic;
using System.Text;

namespace DevFactoryZ.CharityCRM
{
    /// <summary>
    /// Интерфейс определяет компоненты имени подопечного БФ.
    /// </summary>
    public interface IFullName
    {      
        /// <summary>
        /// Имя.
        /// </summary>
        string FirstName { get; }
        
        /// <summary>
        /// Отчество.
        /// </summary>
        string MiddleName { get; }
        
        /// <summary>
        /// Фамилия.
        /// </summary>
        string SurName { get; }

        void Update(IFullName newFullName);
    }
}
