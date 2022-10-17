using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace EventManagement.WebRazorPages.Extensions
{
    public static class SweetAlertHelper
    {
        public static IHtmlContent SweetAlert(this IHtmlHelper helper, string title, string message, string type, string redirectUrl)
        {
            return SweetAlert(helper, title, message, type, redirectUrl, null);
        }

        public static IHtmlContent SweetAlert(this IHtmlHelper helper, string title, string message, string type, string redirectUrl, object? htmlAttributes)
        {
            // Create tag builder
            var builder = new TagBuilder("script");

            // Add attributes
            builder.MergeAttribute("type", "text/javascript");
            builder.InnerHtml.SetHtmlContent(@$"swal.fire({{
                    title: ""{title}"",
                    text: ""{message}"",
                    type: ""{type}""
                }}).then(function () {{
                    window.location = ""{redirectUrl}"";
                }});");

            return builder;
        }
        public static IHtmlContent SweetAlert(this IHtmlHelper helper, string title, string message, string type)
        {
            return SweetAlert(helper, title, message, type, htmlAttributes: null);
        }

        public static IHtmlContent SweetAlert(this IHtmlHelper helper, string title, string message, string type, object? htmlAttributes)
        {
            // Create tag builder
            var builder = new TagBuilder("script");

            // Add attributes
            builder.MergeAttribute("type", "text/javascript");
            builder.InnerHtml.SetHtmlContent(@$"swal.fire({{
                    title: ""{title}"",
                    html: ""<pre>{message}</pre>"",
                    icon: ""{type}""
                }});");

            return builder;
        }
    }
}
