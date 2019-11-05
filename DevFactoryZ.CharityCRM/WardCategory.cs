using System;

namespace DevFactoryZ.CharityCRM
{
    /// <summary>
    /// Категория подопечного благотворительного фонда
    /// </summary>
    class WardCategory
    {
        //Идентификатор категории
        public int ID { get; set; }
        //Идентификатор родительской категории
        public int ParentID { get; set; }
        //Имя категории
        public string Name { get; set; }
    }
}
