﻿using System;

namespace DevFactoryZ.CharityCRM
{
    /// <summary>
    /// Этот класс представляет разрешение, требуемое для выполнение определенной операции
    /// </summary>
    public class Permission : IAmPersistent<int>
    {
        #region .ctor

        protected Permission() // для ORM
        {
        }

        /// <summary>
        /// Создает экземпляр типа DevFactoryZ.CharityCRM.Permission
        /// </summary>
        /// <param name="name">Имя разрешения</param>
        /// <param name="description">Описание разрешения</param>
        public Permission(string name, string description) : this()
        {
            Name =
                !string.IsNullOrWhiteSpace(name) 
                ? name
                : throw new ArgumentException("Имя разрешения не может быть пустым.", nameof(name));

            Description = description;
        }

        #endregion

        #region Идентификация

        /// <summary>
        /// Возвращает идентифтикатор разрешения в хранилище данных
        /// </summary>
        public int Id { get; protected set; }

        /// <summary>
        /// Возвращает признак доступности удаления разрешения из хранилища данных системы
        /// </summary>
        public bool CanBeDeleted => true;

        public override bool Equals(object obj)
        {
            return (obj is Permission permission) && permission.Id == Id;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        #endregion

        #region Описание разрешения

        /// <summary>
        /// Возвращает имя разрешения
        /// </summary>
        public string Name { get; protected set; }

        /// <summary>
        /// Возвращает или задает описание разрешения
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Изменяет наименование разрешения.
        /// </summary>
        /// <param name="newName">Новое имя разрешения.</param>
        public void ChangeNameTo(string newName)
        {
            Name = 
                !string.IsNullOrWhiteSpace(newName) 
                ? newName 
                : throw new ArgumentNullException(nameof(newName), "Не задано новое имя разрешения.");
        }

        public override string ToString()
        {
            return Name;
        }

        #endregion
    }
}
