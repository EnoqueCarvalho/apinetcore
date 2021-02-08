using CrossCuting.Mappings;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrossCuting.Dependencies
{
    public static class MapperDepedency
    {
        public static void AddMapperDepedencies(this IServiceCollection services)
        {
            var config = new AutoMapper.MapperConfiguration(config =>
            {
                config.AddProfile(new DtoToModelProfile());
                config.AddProfile(new EntityToDtoProfile());
                config.AddProfile(new ModelToEntityProfile());
            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
        }
    }
}
