using Domain.Interfaces.Services.User;
using Microsoft.Extensions.DependencyInjection;
using Service.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrossCuting.DependencyInjection
{
    public static class ServiceDependency
    {
        public static void AddServiceDepedencies(this IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ILoginService, LoginService>();
        }
    }
}
