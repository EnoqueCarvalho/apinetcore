using AutoMapper;
using Domain.Dtos.User;
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
        private readonly IMapper mapper;

        public UserService(IRepository<UserEntity> repository,
            IMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public Task<bool> Delete(Guid id)
        {
            return repository.DeleteAsync(id);
        }

        public async Task<UserDto> Get(Guid id)
        {
            var entity = await repository.SelectAsync(id);
            return mapper.Map<UserDto>(entity);
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            var list = await repository.SelectAsync();
            return mapper.Map<IEnumerable<UserDto>>(list);
        }

        public async Task<UserDtoCreateResult> Post(UserDtoCreate user)
        {
            var entity = mapper.Map<UserEntity>(user);
            var result = await repository.InsertAsync(entity);
            return mapper.Map<UserDtoCreateResult>(result);
        }

        public async Task<UserDtoUpdateResult> Put(UserDtoUpdate user)
        {
            var entity = mapper.Map<UserEntity>(user);
            var result = await repository.UpdateAsync(entity);
            return mapper.Map<UserDtoUpdateResult>(result);
        }
    }
}
