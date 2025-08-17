using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ShahanStore.RazorPage.TagHelpers;

[HtmlTargetElement("open-modal")]
public class OpenModalTagHelper : TagHelper
{
    private readonly IUrlHelperFactory _urlHelperFactory;
    public OpenModalTagHelper(IUrlHelperFactory urlHelperFactory)
    {
        _urlHelperFactory = urlHelperFactory;
    }

    [ViewContext]
    [HtmlAttributeNotBound]
    public ViewContext ViewContext { get; set; }

    // --- پارامترهای تگ هلپر ---
    [HtmlAttributeName("asp-page")]
    public string Page { get; set; }

    [HtmlAttributeName("asp-page-handler")]
    public string PageHandler { get; set; }

    [HtmlAttributeName("asp-route-id")]
    public Guid ItemId { get; set; }

    public string ModalTitle { get; set; } = "";

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);

        // ساخت URL به صورت خودکار
        var url = urlHelper.Page(Page, PageHandler, new { id = ItemId });

        output.TagName = "a";
        output.Attributes.SetAttribute("type", "a");

        output.Attributes.SetAttribute("data-url", url);
        output.Attributes.SetAttribute("data-title", ModalTitle);

        var existingClass = context.AllAttributes["class"]?.Value.ToString() ?? "";
        output.Attributes.SetAttribute("class", $"btn open-modal-btn {existingClass}");
    }
}