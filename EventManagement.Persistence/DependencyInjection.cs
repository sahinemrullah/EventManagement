using EventManagement.Domain.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EventManagement.Persistence
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<EventManagementDbContext>(options =>
                options.UseSqlServer(
                    configuration.GetConnectionString("EventManagementDB"),
                        b => b.MigrationsAssembly(typeof(EventManagementDbContext).Assembly.FullName)));

            services.AddScoped<IUnitOfWork>(serviceProvider => serviceProvider.GetRequiredService<EventManagementDbContext>());
            return services;
        }
    }
}
