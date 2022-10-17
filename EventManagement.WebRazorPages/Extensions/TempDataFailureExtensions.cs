using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace EventManagement.WebRazorPages.Extensions
{
    public static class TempDataFailureExtensions
    {
        private const string _failureMessageKey = "Failure";
        public static string? GetFailureMessage(this ITempDataDictionary tempData)
        {
            if (!tempData.ContainsKey(_failureMessageKey) || tempData[_failureMessageKey] is not string failureMessage)
                return null;

            return failureMessage;
        }
        public static void SetFailureMessage(this ITempDataDictionary tempData, string message)
        {

            if (tempData.ContainsKey(_failureMessageKey))
                tempData[_failureMessageKey] = message;
            else
                tempData.Add(_failureMessageKey, message);
        }
        public static IHtmlContent ShowSwalFailureMessage(this IHtmlHelper helper, ITempDataDictionary tempData)
        {
            string? failureMessage = tempData.GetFailureMessage();

            if (failureMessage is not null)
                return SweetAlertHelper.SweetAlert(helper, "Something went wrong.", failureMessage, "error");

            HtmlString htmlString = new(string.Empty);

            return htmlString;
        }
        public static IHtmlContent ShowSwalFailureMessage(this IHtmlHelper helper, ITempDataDictionary tempData, string redirectUrl)
        {
            string? failureMessage = tempData.GetFailureMessage();

            if (failureMessage is not null)
                return SweetAlertHelper.SweetAlert(helper, "Something went wrong.", failureMessage, "error", redirectUrl);

            HtmlString htmlString = new(string.Empty);

            return htmlString;
        }
    }
}
