﻿using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DevFactoryZ.CharityCRM.Persistence.EFCore
{
    internal class PermissionRepository : IPermissionRepository
    {
        private readonly DbSet<Permission> setOfPermissions;

        private readonly Action save;

        public PermissionRepository(DbSet<Permission> setOfPermissions, Action save)
        {
            this.setOfPermissions = setOfPermissions;
            this.save = save;
        }

        public void Add(Permission permission)
        {
            setOfPermissions.Add(permission);
        }

        public void Delete(int id)
        {
            setOfPermissions.Remove(GetById(id));
        }

        public IEnumerable<Permission> GetAll()
        {
            return setOfPermissions.ToArray();
        }

        public Permission GetById(int id)
        {
            return setOfPermissions.Find(id) 
                ?? throw new EntityNotFoundException(id, typeof(Permission));
        }

        public void Save()
        {
            save();
        }
    }
}
