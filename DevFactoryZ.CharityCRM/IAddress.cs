using System;
using System.Collections.Generic;
using System.Text;

namespace DevFactoryZ.CharityCRM
{
    /// <summary>
    /// Представляет компоненты почтового адреса.
    /// </summary>
    public interface IAddress
    {
        /// <summary>
        /// Почтовый индекс
        /// </summary>
        string PostCode { get; }
        
        /// <summary>
        /// Страна
        /// </summary>
        string Country { get; }
        
        /// <summary>
        /// Область, край
        /// </summary>
        string Region { get; }
        
        /// <summary>
        /// Город
        /// </summary>
        string City { get; }
        
        /// <summary>
        /// Район
        /// </summary>
        string Area { get; }
        
        /// <summary>
        /// Улица
        /// </summary>
        string Street { get; }
        
        /// <summary>
        /// Дом
        /// </summary>
        string House { get; }
        
        /// <summary>
        /// Квартира
        /// </summary>
        string Flat { get; }

        void Update(IAddress newAddress);
    }
}
