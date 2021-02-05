using Domain.Dtos;
using Domain.Entities;
using Domain.Interfaces.Services.User;
using Domain.Repositories;
using Domain.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Service.Services
{
    public class LoginService : ILoginService
    {
        private readonly IUserRepository userRepository;
        private readonly SigningConfigurations signingConfiguration;

        public LoginService(IUserRepository userRepository,
            SigningConfigurations signingConfiguration)
        {
            this.userRepository = userRepository;
            this.signingConfiguration = signingConfiguration;
        }

        public async Task<object> FindByLoginAsync(LoginDto user)
        {
            if (user == null || string.IsNullOrEmpty(user.Email))
                return null;

            var result = await userRepository.FindByLoginAsync(user.Email);

            if (result == null)
                return null;

            var identity = new ClaimsIdentity(
                new GenericIdentity(result.Email),
                new[]
                {
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.UniqueName, result.Email)
                });

            var createDate = DateTime.Now;
            var expirationDate = createDate.AddMinutes(120);
            var handler = new JwtSecurityTokenHandler();
            var token = CreateToken(identity, createDate, expirationDate, handler);

            return new
            {
                created = createDate.ToString(),
                expiration = expirationDate.ToString(),
                accessToken = token
            };
        }

        private string CreateToken(ClaimsIdentity identity, DateTime createDate, DateTime expirationDate, JwtSecurityTokenHandler handler)
        {
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                SigningCredentials = signingConfiguration.SigningCredentials,
                Subject = identity,
                NotBefore = createDate,
                Expires = expirationDate
            });

            return handler.WriteToken(securityToken);
        }
    }
}
