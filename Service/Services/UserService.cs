using Domain.Entities;
using Domain.Interfaces;
using Domain.Interfaces.Services.User;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<UserEntity> repository;

        public UserService(IRepository<UserEntity> repository)
        {
            this.repository = repository;
        }

        public Task<bool> Delete(Guid id)
        {
            return repository.DeleteAsync(id);
        }

        public Task<UserEntity> Get(Guid id)
        {
            return repository.SelectAsync(id);
        }

        public Task<IEnumerable<UserEntity>> GetAll()
        {
            return repository.SelectAsync();
        }

        public Task<UserEntity> Post(UserEntity user)
        {
            return repository.InsertAsync(user);
        }

        public Task<UserEntity> Put(UserEntity user)
        {
            return repository.UpdateAsync(user);
        }
    }
}
