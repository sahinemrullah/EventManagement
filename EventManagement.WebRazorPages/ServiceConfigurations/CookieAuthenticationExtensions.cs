using Microsoft.AspNetCore.Authentication.Cookies;

namespace EventManagement.WebRazorPages.ServiceConfigurations
{
    public static class CookieAuthenticationExtensions
    {
        public static void ConfigureCookieAuthentication(this CookieAuthenticationOptions options)
        {
            options.Cookie.HttpOnly = true;
            options.ExpireTimeSpan = TimeSpan.FromMinutes(10);
            options.SlidingExpiration = true;
            options.AccessDeniedPath = "/Auth/Forbidden/";
            options.LoginPath = "/Auth/Login";
            options.LogoutPath = "/Auth/Logout";
        }
    }
}
