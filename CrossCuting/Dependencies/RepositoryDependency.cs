using Data.Contexts;
using Data.Implementations;
using Data.Repository;
using Domain.Interfaces;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrossCuting.DependencyInjection
{
    public static class RepositoryDependency
    {
        public static void AddRepositoryDepedencies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUserRepository, UserImplementation>();

            services.AddDbContext<ApplicationContext>(options =>
            {
                options.UseMySql(configuration.GetConnectionString("Default"));
            });
        }
    }
}
