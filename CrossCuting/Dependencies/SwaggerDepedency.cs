using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CrossCuting.Dependencies
{
    public static class SwaggerDepedency
    {
        public static void AddSwaggerDepedencies(this IServiceCollection services)
        {
            services.AddSwaggerGen(setup =>
            {
                setup.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "Curso de API com AspNetCore 3.1",
                    Description = "Arquitetura DDD",
                    TermsOfService = new Uri("http://localhost:5000/"),
                    Contact = new OpenApiContact
                    {
                        Name = "Enoque Carvalho",
                        Email = "enoque.carvalho5@gmail.com",
                        Url = new Uri("http://localhost:5000/"),
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Termo de Licença",
                        Url = new Uri("http://localhost:5000/")
                    }
                });
            });
        }

        public static void UseSwagger(this IApplicationBuilder app, IConfiguration configuration)
        {
            app.UseSwagger();
            app.UseSwaggerUI(setup =>
            {
                setup.SwaggerEndpoint("/swagger/v1/swagger.json", configuration.GetSection("Swagger").GetSection("EndpointName").Value);
                setup.RoutePrefix = string.Empty;
            });
        }
    }
}
