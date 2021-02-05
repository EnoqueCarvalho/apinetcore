using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IUserRepository : IRepository<UserEntity>
    {
        Task<UserEntity> FindByLoginAsync(string email);
    }
}
