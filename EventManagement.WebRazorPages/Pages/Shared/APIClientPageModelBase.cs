using EventManagement.WebRazorPages.Extensions;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Net;
using System.Text.Json;

namespace EventManagement.WebRazorPages.Pages.Shared
{
    public abstract class APIClientPageModelBase : PageModel
    {
        protected HttpClient HttpClient { get; }
        protected string DeleteRedirectPage { get; set; }
        protected string PatchRedirectPage { get; set; }
        protected string PartialViewName { get; set; }

        public APIClientPageModelBase(IHttpClientFactory httpClientFactory)
        {
            DeleteRedirectPage = "./Index";
            PatchRedirectPage = "./Index";
            PartialViewName = string.Empty;
            HttpClient = httpClientFactory.GetAPIClient();
        }
        protected async Task<IActionResult> GetAsync(string uri, Func<HttpContent, Task<IActionResult>> next)
        {
            HttpResponseMessage response = await HttpClient.GetAsync(uri);

            if (!response.IsSuccessStatusCode)
                return await GetStatusCodeResultAsync(response);

            return await next.Invoke(response.Content);
        }
        protected async Task<IActionResult> PostAsync<T>(string uri, T value, Func<HttpContent, Task<IActionResult>> next)
        {
            if (!ModelState.IsValid)
                return GetModelStateResult();

            HttpResponseMessage response = await HttpClient.PostAsJsonAsync(uri, value);

            if (!response.IsSuccessStatusCode)
                return await GetStatusCodeResultAsync(response);

            return await next.Invoke(response.Content);
        }
        protected async Task<IActionResult> DeleteAsync(string uri)
        {
            HttpResponseMessage response = await HttpClient.DeleteAsync(uri);

            if (!response.IsSuccessStatusCode)
                return await GetStatusCodeResultAsync(response);

            if (Request.IsAjaxRequest())
                return new OkObjectResult(new { Message = "Successfully deleted.", RedirectUrl = Url.Page(DeleteRedirectPage) });

            TempData.SetSuccessMessage("Successfully deleted.");

            return RedirectToPage(DeleteRedirectPage);
        }

        protected async Task<IActionResult> PutAsync<T>(string uri, T data)
        {
            if (!ModelState.IsValid)
                return Page();

            HttpResponseMessage response = await HttpClient.PutAsJsonAsync(uri, data);

            if (!response.IsSuccessStatusCode)
                return await GetStatusCodeResultAsync(response);

            TempData.SetSuccessMessage("Successfully updated.");

            return Page();
        }

        protected async Task<IActionResult> PatchAsync<T>(string uri, T data)
        {
            if (!ModelState.IsValid)
                return Page();

            string jsonData = JsonSerializer.Serialize(data);

            HttpContent content = new StringContent(jsonData);

            HttpResponseMessage response = await HttpClient.PatchAsync(uri, content);

            if (!response.IsSuccessStatusCode)
                return await GetStatusCodeResultAsync(response);

            TempData.SetSuccessMessage("Successfully updated.");

            return RedirectToPage(PatchRedirectPage);
        }
        protected async Task<IActionResult> GetStatusCodeResultAsync(HttpResponseMessage response)
        {
            if (response.IsSuccessStatusCode)
                throw new InvalidOperationException();

            return response.StatusCode switch
            {
                HttpStatusCode.NotFound => new NotFoundResult(),
                HttpStatusCode.BadRequest => ParseModelState(await response.Content.ReadAsStringAsync()),
                HttpStatusCode.Forbidden => new ForbidResult(CookieAuthenticationDefaults.AuthenticationScheme),
                HttpStatusCode.Unauthorized => new ChallengeResult(CookieAuthenticationDefaults.AuthenticationScheme),
                _ => new StatusCodeResult(500)
            };
        }
        private readonly static JsonSerializerOptions options = new() { PropertyNameCaseInsensitive = true };
        private IActionResult ParseModelState(string responseMessage)
        {
            try
            {
                ErrorResult? errors = JsonSerializer.Deserialize<ErrorResult>(responseMessage, options);
                if (errors is not null)
                {
                    ParseModelState(errors.Errors);
                }
            }
            catch (Exception)
            {
                TempData.SetFailureMessage($"Something went wrong while processing your request please try again.");
            }

            return GetModelStateResult();
        }

        private IActionResult GetModelStateResult()
        {
            if (Request.IsAjaxRequest())
            {
                if (!Request.IsSupportedAcceptType())
                    return StatusCode(StatusCodes.Status406NotAcceptable);

                if (Request.AcceptsHtml())
                {
                    HttpContext.Response.StatusCode = StatusCodes.Status400BadRequest;
                    return Partial(PartialViewName);
                }

                return BadRequest(ModelState);
            }

            return Page();
        }

        private void ParseModelState(Dictionary<string, string[]> keyValuePairs)
        {
            foreach (var (key, errorList) in keyValuePairs)
            {
                string actualKey = ModelState.Keys
                                            .Where(k => k.Contains(key, StringComparison.InvariantCultureIgnoreCase))
                                            .FirstOrDefault(string.Empty);
                foreach (string value in errorList)
                {
                    ModelState.AddModelError(actualKey, value);
                }
            }
        }
    }
}
