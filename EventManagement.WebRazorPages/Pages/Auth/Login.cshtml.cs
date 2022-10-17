using EventManagement.WebRazorPages.Pages.Shared;
using EventManagement.WebRazorPages.ServiceConfigurations;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EventManagement.WebRazorPages.Pages.Auth
{
    public class LoginModel : APIClientPageModelBase
    {
        public LoginModel(IHttpClientFactory httpClientFactory, IUserAccessor userAccessor) : base(httpClientFactory, userAccessor)
        {
        }

        [BindProperty]
        public string Email { get; set; } = null!;
        [BindProperty]
        public string Password { get; set; } = null!;
        [BindProperty]
        public bool RememberMe { get; set; }
        public IActionResult OnGet()
        {
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            return await PostAsync("api/users/login", new { Email, Password }, OnPostAsyncHandler);
        }

        public async Task<IActionResult> OnPostAsyncHandler(HttpContent httpContent)
        {
            AccessToken? accessToken = await httpContent.ReadFromJsonAsync<AccessToken>();
            if (accessToken != null)
            {
                var token = accessToken.Token;
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(token);

                ICollection<Claim> claims = jwtSecurityToken.Claims.Select(c => new Claim(c.Type, c.Value)).ToList();
                claims.Add(new Claim("Access-Token", accessToken.Token));
                claims.Add(new Claim("Refresh-Token", accessToken.RefreshToken));

                ClaimsIdentity claimsIdentity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                ClaimsPrincipal claimsPrincipal = new(claimsIdentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, new AuthenticationProperties() { IsPersistent = RememberMe, AllowRefresh = true });
            }

            return RedirectToPage("/Index");
        }
    }
}
