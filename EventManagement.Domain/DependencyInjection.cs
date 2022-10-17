using EventManagement.Domain.Common;
using EventManagement.Domain.Entities;
using EventManagement.Domain.Validations;
using EventManagement.Domain.Validations.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace EventManagement.Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomain(this IServiceCollection services)
        {
            services.AddScoped<IDefinitionEntityCreator<Category>, CategoryCreator>();
            services.AddScoped<IDefinitionEntityCreator<City>, CityCreator>();
            services.AddScoped<IResultFactory, ResultFactory>();

            return services;
        }
    }
}
