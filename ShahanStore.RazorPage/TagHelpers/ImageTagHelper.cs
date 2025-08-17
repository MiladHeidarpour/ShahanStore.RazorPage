using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using ShahanStore.RazorPage.Infrastructure;

namespace ShahanStore.RazorPage.TagHelpers;


//[HtmlTargetElement("img", Attributes = "api-src")]
//public class ImageTagHelper : TagHelper
//{
//    private readonly IUrlBuilder _urlBuilder;

//    public ImageTagHelper(IUrlBuilder urlBuilder)
//    {
//        _urlBuilder = urlBuilder;
//    }

//    public string ApiSrc { get; set; }

//    public override void Process(TagHelperContext context, TagHelperOutput output)
//    {
//        if (!string.IsNullOrWhiteSpace(ApiSrc))
//        {
//            var finalUrl = _urlBuilder.BuildApiUrl(ApiSrc);
//            output.Attributes.SetAttribute("src", finalUrl);
//        }
//    }
//}



[HtmlTargetElement("img", Attributes = "api-src")]
public class ImageTagHelper : TagHelper
{
    private readonly IUrlHelperFactory _urlHelperFactory;
    private readonly IUrlBuilder _urlBuilder;

    public ImageTagHelper(IUrlHelperFactory urlHelperFactory, IUrlBuilder urlBuilder)
    {
        _urlHelperFactory = urlHelperFactory;
        _urlBuilder = urlBuilder;
    }

    [ViewContext]
    [HtmlAttributeNotBound]
    public ViewContext ViewContext { get; set; }

    // --- پراپرتی اصلی برای آدرس تصویر ---
    public string ApiSrc { get; set; }

    // --- پراپرتی‌های جدید برای قابلیت مودال ---
    [HtmlAttributeName("modal-page")]
    public string? ModalPage { get; set; }

    [HtmlAttributeName("modal-page-handler")]
    public string? ModalPageHandler { get; set; }

    [HtmlAttributeName("modal-route-id")]
    public Guid? ModalRouteId { get; set; }

    [HtmlAttributeName("modal-title")]
    public string? ModalTitle { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        // ۱. ابتدا آدرس کامل تصویر را مانند قبل می‌سازیم
        if (!string.IsNullOrWhiteSpace(ApiSrc))
        {
            var finalImageUrl = _urlBuilder.BuildApiUrl(ApiSrc);
            output.Attributes.SetAttribute("src", finalImageUrl);
        }

        // ۲. حالا چک می‌کنیم که آیا این تصویر باید یک دکمه مودال باشد یا نه
        if (!string.IsNullOrWhiteSpace(ModalPage) && !string.IsNullOrWhiteSpace(ModalPageHandler))
        {
            var urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
            var modalUrl = urlHelper.Page(ModalPage, ModalPageHandler, new { id = ModalRouteId });

            // ۳. تگ <img> را با یک تگ <a> بسته‌بندی می‌کنیم
            output.PreElement.AppendHtml($"<a href='javascript:void(0);' class='open-modal-btn' data-url='{modalUrl}' data-title='{ModalTitle}'>");
            output.PostElement.AppendHtml("</a>");

            // ۴. برای زیبایی بیشتر، استایل اشاره‌گر را به تصویر اضافه می‌کنیم
            var existingStyle = output.Attributes["style"]?.Value.ToString() ?? "";
            output.Attributes.SetAttribute("style", $"{existingStyle} cursor:pointer;");
        }
    }
}