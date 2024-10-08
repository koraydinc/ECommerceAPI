﻿using ECommerceAPI.Application.Repositories;
using ECommerceAPI.Domain.Entities.Common;
using ECommerceAPI.Persistence.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceAPI.Persistence.Repositories
{
    public class WriteRepository<T> : IWriteRepository<T> where T : BaseEntitiy
    {
        private readonly ECommerceAPIDbContext _context;
        public WriteRepository(ECommerceAPIDbContext context)
        {
            _context = context;
        }
        public DbSet<T> Table => _context.Set<T>();
        public async Task<bool> AddAsync(T data)
        {
            EntityEntry entityEntry = await Table.AddAsync(data);
            return entityEntry.State == EntityState.Added;
        }
        public async Task<bool> AddRangeAsync(List<T> datas)
        {
            await Table.AddRangeAsync(datas);
            return true;
        }
        public bool Remove(T data)
        {
            EntityEntry entityEntry = Table.Remove(data);
            return entityEntry.State == EntityState.Deleted;
        }
        public bool RemoveRange(List<T> datas)
        {
            Table.RemoveRange(datas);
            return true;
        }
        public async Task<bool> RemoveAsync(string id)
        {
            T model = await Table.FirstOrDefaultAsync(data => data.Id == Guid.Parse(id));
            return Remove(model);
        }
        public bool Update(T data)
        {
            EntityEntry entityEntry = Table.Update(data);
            return entityEntry.State == EntityState.Modified;
        }
        public async Task<int> SaveAsync()
            => await _context.SaveChangesAsync();

        
    }
}
