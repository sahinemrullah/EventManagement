using Microsoft.Net.Http.Headers;

namespace EventManagement.WebRazorPages.Extensions
{
    public static class HttpRequestExtensions
    {
        public static bool IsAjaxRequest(this HttpRequest httpRequest)
        {
            const string XMLHttpRequest = nameof(XMLHttpRequest);

            return string.Equals(httpRequest.Headers.XRequestedWith, XMLHttpRequest, StringComparison.Ordinal) ||
                string.Equals(httpRequest.Query[HeaderNames.XRequestedWith], XMLHttpRequest, StringComparison.Ordinal);
        }

        public static bool AcceptsHtml(this HttpRequest httpRequest)
        {
            return httpRequest.Headers.Accept.ToString().Contains("text/html", StringComparison.Ordinal);
        }

        public static bool IsSupportedAcceptType(this HttpRequest httpRequest)
        {
            string[] supportedHeaders = new string[] { "application/json", "text/html" };

            string acceptHeader = httpRequest.Headers.Accept.ToString();

            return supportedHeaders
                                    .Any(header => acceptHeader.Contains(header, StringComparison.Ordinal));
        }
    }
}
