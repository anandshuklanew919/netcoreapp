using Microsoft.AspNetCore.Razor.TagHelpers;

namespace required.Helpers
{
    [HtmlTargetElement("custom-email")]
    public class CustomEmailTagHelper : TagHelper
    {
        public string MyEmail { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            output.TagName = "a";
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Attributes.SetAttribute("href", $"mailto:{MyEmail}");
            output.Content.SetContent("Anand Shukla");
        }
    }
}
