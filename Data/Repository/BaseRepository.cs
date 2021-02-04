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
            try
            {
                var result = await dataSet.FirstOrDefaultAsync(p => p.Id.Equals(id));

                if (result == null)
                    return false;

                dataSet.Remove(result);
                await context.SaveChangesAsync();

                return true;
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task<T> InsertAsync(T item)
        {
            try
            {
                if (item.Id == Guid.Empty)
                    item.Id = Guid.NewGuid();

                item.CreateAt = DateTime.UtcNow;
                dataSet.Add(item);
                await context.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return item;
        }

        public async Task<T> SelectAsync(Guid id)
        {
            try
            {
                return await dataSet.SingleOrDefaultAsync(p => p.Id.Equals(id));
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task<IEnumerable<T>> SelectAsync()
        {
            try
            {
                return await dataSet.ToListAsync();
            }
            catch (Exception exception)
            {
                throw exception;
            }
        }

        public async Task<T> UpdateAsync(T item)
        {
            try
            {
                var result = await dataSet.FirstOrDefaultAsync(p => p.Id.Equals(item.Id));

                if (result == null)
                    return null;

                item.UpdateAt = DateTime.UtcNow;
                item.CreateAt = result.CreateAt;
                context.Entry(result).CurrentValues.SetValues(item);
                await context.SaveChangesAsync();
            }
            catch (Exception exception)
            {
                throw exception;
            }

            return item;
        }

        public async Task<bool> ExistAsync(Guid id)
        {
            return await dataSet.AnyAsync(p => p.Id.Equals(id));
        }
    }
}
