using EventManagement.WebRazorPages.Pages.Models;
using EventManagement.WebRazorPages.Pages.Shared;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace EventManagement.WebRazorPages.Pages.Auth
{
    public class LoginModel : APIClientPageModelBase
    {
        public LoginModel(IHttpClientFactory httpClientFactory) : base(httpClientFactory)
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
            return await PostAsync(API.User.Login, new { Email, Password }, OnPostAsyncHandler);
        }

        public async Task<IActionResult> OnPostAsyncHandler(HttpContent httpContent)
        {
            AccessToken? accessToken = await httpContent.ReadFromJsonAsync<AccessToken>();
            if (accessToken != null)
            {
                var token = accessToken.Token;
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(token);

                ICollection<Claim> claims = jwtSecurityToken.Claims.ToList();
                claims.Add(new Claim(API.AccessTokenKey, accessToken.Token));
                claims.Add(new Claim(API.RefreshTokenKey, accessToken.RefreshToken));

                ClaimsIdentity claimsIdentity = new(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                ClaimsPrincipal claimsPrincipal = new(claimsIdentity);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal, new AuthenticationProperties() { IsPersistent = RememberMe, AllowRefresh = true });
            }

            return RedirectToPage("/Index");
        }
    }
}
