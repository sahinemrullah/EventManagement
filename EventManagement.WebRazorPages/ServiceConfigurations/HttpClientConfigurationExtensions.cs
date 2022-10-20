using EventManagement.WebRazorPages.Pages.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.Security.Claims;

namespace EventManagement.WebRazorPages.ServiceConfigurations
{
    public static class HttpClientConfigurationExtensions
    {
        public static IServiceCollection AddAPIHttpClient(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();
            
            services.AddHttpClient(API.ClientName, (serviceProvider, config) =>
            {
                config.BaseAddress = new Uri(API.ClientBaseAddress);
                
                IHttpContextAccessor httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();
                
                ClaimsPrincipal principal = httpContextAccessor.HttpContext!.User;
                
                if (principal.Identity is not null && principal.Identity.IsAuthenticated)
                {
                    string accessToken = principal.FindFirstValue(API.AccessTokenKey);
                    config.DefaultRequestHeaders.Authorization = new(JwtBearerDefaults.AuthenticationScheme, accessToken);
                }
            });

            return services;
        }
    }
}
