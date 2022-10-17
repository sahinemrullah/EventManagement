using System.Security.Claims;

namespace EventManagement.WebRazorPages.ServiceConfigurations
{
    public class UserAccessor : IUserAccessor
    {
        private readonly IHttpContextAccessor _accessor;

        public UserAccessor(IHttpContextAccessor accessor)
        {
            _accessor = accessor ?? throw new ArgumentNullException(nameof(accessor));
        }

        public ClaimsPrincipal User => _accessor!.HttpContext!.User;
    }

    public static class UserAccessorExtensions
    {
        public static IServiceCollection AddUserAccessor(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddScoped<IUserAccessor, UserAccessor>();

            return services;
        }
    }
}
