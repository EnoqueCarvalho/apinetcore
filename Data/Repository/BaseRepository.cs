using Data.Contexts;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repository
{
    public class BaseRepository<T> : IRepository<T>
        where T : BaseEntity
    {
        private readonly ApplicationContext context;
        private DbSet<T> dataSet;

        public BaseRepository(ApplicationContext context)
        {
            this.context = context;
            this.dataSet = context.Set<T>();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await dataSet.FirstOrDefaultAsync(p => p.Id.Equals(id));

            if (result == null)
                return false;

            dataSet.Remove(result);
            await context.SaveChangesAsync();

            return true;
        }

        public async Task<T> InsertAsync(T item)
        {
            if (item.Id == Guid.Empty)
                item.Id = Guid.NewGuid();

            item.CreateAt = DateTime.UtcNow;
            dataSet.Add(item);
            await context.SaveChangesAsync();

            return item;
        }

        public async Task<T> SelectAsync(Guid id)
        {
            return await dataSet.SingleOrDefaultAsync(p => p.Id.Equals(id));
        }

        public async Task<IEnumerable<T>> SelectAsync()
        {
            return await dataSet.ToListAsync();
        }

        public async Task<T> UpdateAsync(T item)
        {
            var result = await dataSet.FirstOrDefaultAsync(p => p.Id.Equals(item.Id));

            if (result == null)
                return null;

            item.UpdateAt = DateTime.UtcNow;
            item.CreateAt = result.CreateAt;
            context.Entry(result).CurrentValues.SetValues(item);
            await context.SaveChangesAsync();

            return item;
        }

        public async Task<bool> ExistAsync(Guid id)
        {
            return await dataSet.AnyAsync(p => p.Id.Equals(id));
        }
    }
}
