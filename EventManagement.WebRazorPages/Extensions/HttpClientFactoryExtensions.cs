using EventManagement.WebRazorPages.Pages.Shared;

namespace EventManagement.WebRazorPages.Extensions
{
    public static class HttpClientFactoryExtensions
    {
        public static HttpClient GetAPIClient(this IHttpClientFactory factory)
        {
            HttpClient client = factory.CreateClient(API.ClientName);
            return client;
        }
    }
}
