using DevFactoryZ.CharityCRM.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevFactoryZ.CharityCRM.Services
{
    public class WardCategoryService : IWardCategoryService
    {
        private readonly IWardCategoryRepository repository;

        public WardCategoryService(IWardCategoryRepository repository)
        {
            this.repository = repository;
        }

        public WardCategory Create(WardCategory wardCategory)
        {
            repository.Add(wardCategory);
            repository.Save();

            return wardCategory;
        }

        public void Delete(int id)
        {
            repository.Delete(id);
            repository.Save();
        }

        public void RemoveChild(int parentId, int childId)
        {
            var child = GetById(childId);

            var parent = GetById(parentId);
            
            parent.RemoveChild(child);

            repository.Save();
        }

        public IEnumerable<WardCategory> GetAll()
        {
            return repository.GetAll();
        }

        public WardCategory GetById(int id)
        {
            return repository.GetById(id);
        }

        public void AddChild(int parentId, int childId)
        {
            if (HasParent(childId))
            {
                throw new ArgumentException(
                    $"Категория '{childId}' уже имеет родителя.", nameof(childId));
            }

            if (HasCycling(parentId, childId))
            {
                throw new ArgumentException(
                    $"Циклическая ссылка. Нельзя категорию '{childId}' назначать подкатегорией для категории '{parentId}'.");
            }

            var child = GetById(childId);

            var parent = GetById(parentId);

            parent.AddChild(child);

            repository.Save();
        }

        public void Update(int id, string newName)
        {
            if (string.IsNullOrWhiteSpace(newName))
            {
                throw new ArgumentNullException(
                    nameof(newName), "Наименование категории подопечного не может быть пустым.");
            }

            var updated = repository.GetById(id);
            
            updated.Name = newName;
            
            repository.Save();
        }

        public bool HasParent(int wardCategoryId)
        {
            return repository.HasParent(wardCategoryId);
        }

        public bool HasCycling(int parentId, int childId)
        {
            return repository.HasCycling(parentId, childId);
        }
    }
}
