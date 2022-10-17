using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace EventManagement.WebRazorPages.Extensions
{
    public static class TempDataSuccessExtensions
    {
        private const string _successMessageKey = "SuccessMessage";
        public static string? GetSuccessMessage(this ITempDataDictionary tempData)
        {
            if (!tempData.ContainsKey(_successMessageKey) || tempData[_successMessageKey] is not string successMessage)
                return null;

            return successMessage;
        }
        public static void SetSuccessMessage(this ITempDataDictionary tempData, string message)
        {
            if(tempData.ContainsKey(_successMessageKey))
                tempData[_successMessageKey] = message;
            else
                tempData.Add(_successMessageKey, message);
        }
        public static IHtmlContent ShowSwalSuccessMessage(this IHtmlHelper helper, ITempDataDictionary tempData, string redirectUrl)
        {
            string? successMessage = tempData.GetSuccessMessage();

            if (successMessage is not null)
                return SweetAlertHelper.SweetAlert(helper, "Success", successMessage, "success", redirectUrl);

            HtmlString htmlString = new(string.Empty);

            return htmlString;
        }
        public static IHtmlContent ShowSwalSuccessMessage(this IHtmlHelper helper, ITempDataDictionary tempData)
        {
            string? successMessage = tempData.GetSuccessMessage();

            if (successMessage is not null)
                return SweetAlertHelper.SweetAlert(helper, "Success", successMessage, "success");

            HtmlString htmlString = new(string.Empty);

            return htmlString;
        }
    }
}
