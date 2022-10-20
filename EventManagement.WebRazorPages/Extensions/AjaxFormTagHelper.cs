using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace EventManagement.WebRazorPages.Extensions
{
    public class AjaxFormTagHelper : FormTagHelper
    {
        public string Button { get; set; }
        public string ButtonText { get; set; }
        public string ButtonType { get; set; }
        public bool? CenterButton { get; set; }
        public string Type { get; set; }
        public AjaxFormTagHelper(IHtmlGenerator generator) : base(generator)
        {

        }
        public override async Task ProcessAsync(TagHelperContext context, TagHelperOutput output)
        {
            await base.ProcessAsync(context, output);


            output.TagName = "form";

            output.Attributes.SetAttribute("data-ajax", "true");

            output.Attributes.SetAttribute("data-ajax-dataType", Type);

            output.Attributes.SetAttribute("data-ajax-button", $"#{Button}");

            output.Attributes.SetAttribute("data-ajax-success", "success");

            output.Attributes.SetAttribute("data-ajax-failure", "failure");

            output.Attributes.SetAttribute("method", "post");

            output.Attributes.SetAttribute("data-ajax-method", "post");


            TagBuilder submitButton = new("button");

            submitButton.Attributes.Add("id", Button);

            submitButton.Attributes.Add("type", "submit");

            submitButton.Attributes.Add("data-loading-text", "Loading...");

            submitButton.Attributes.Add("data-text", ButtonText);

            submitButton.AddCssClass($"btn btn-{ButtonType}");

            submitButton.InnerHtml.SetContent(ButtonText);


            var content = await output.GetChildContentAsync();

            output.Content.SetHtmlContent(content.GetContent());


            if (CenterButton.GetValueOrDefault(true))
            {
                TagBuilder submitButtonDiv = new("div");

                submitButtonDiv.AddCssClass("text-center mt-3");

                submitButtonDiv.InnerHtml.SetHtmlContent(submitButton);

                output.Content.AppendHtml(submitButtonDiv);
            }
            else
            {
                output.Content.AppendHtml(submitButton);
            }
        }
    }
}
