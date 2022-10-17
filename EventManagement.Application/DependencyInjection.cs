using EventManagement.Application.Features.DefinitionEntities;
using EventManagement.Application.Features.Events;
using EventManagement.Application.Features.IntegrationServices;
using EventManagement.Application.Features.Mail;
using EventManagement.Application.Features.Users;
using EventManagement.Application.Security;
using EventManagement.Domain;
using EventManagement.Domain.Common;
using EventManagement.Domain.Entities;
using EventManagement.Domain.Security;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Configuration;

namespace EventManagement.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplication(this IServiceCollection services)
        {
            services.AddDomain();

            services.AddScoped<IEmailService, EmailService>();

            services.AddScoped<IDefinitionEntityService<Category>, DefinitionEntityService<Category, IDefinitionEntityCreator<Category>>>();
            
            services.AddScoped<IDefinitionEntityService<City>, DefinitionEntityService<City, IDefinitionEntityCreator<City>>>();
            
            services.AddScoped<IPasswordHasher, PasswordHasher>();
            
            services.AddScoped<IUsersService, UsersService>();
            
            services.AddScoped<IIntegrationServicesService, IntegrationServicesService>();
            
            services.AddScoped<IEventService, EventService>();


            return services;
        }
    }
}
