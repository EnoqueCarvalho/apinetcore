using Data.Contexts;
using Data.Repository;
using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Data.Implementations
{
    public class UserImplementation : BaseRepository<UserEntity>, IUserRepository
    {
        private readonly ApplicationContext context;
        private DbSet<UserEntity> dbSet;

        public UserImplementation(ApplicationContext context) : base(context)
        {
            this.dbSet = context.Set<UserEntity>();
        }

        public async Task<UserEntity> FindByLoginAsync(string email)
        {
            return await dbSet.FirstOrDefaultAsync(u => u.Email.Equals(email));
        }
    }
}
