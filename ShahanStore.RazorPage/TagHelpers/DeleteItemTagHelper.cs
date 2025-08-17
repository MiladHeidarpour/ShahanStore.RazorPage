using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace ShahanStore.RazorPage.TagHelpers;

[HtmlTargetElement("delete_Item")]
public class DeleteItemTagHelper : TagHelper
{
    private readonly IUrlHelperFactory _urlHelperFactory;
    public DeleteItemTagHelper(IUrlHelperFactory urlHelperFactory)
    {
        _urlHelperFactory = urlHelperFactory;
    }

    [ViewContext]
    [HtmlAttributeNotBound]
    public ViewContext ViewContext { get; set; }


    [HtmlAttributeName("asp-page")]
    public string Page { get; set; }

    [HtmlAttributeName("asp-page-handler")]
    public string PageHandler { get; set; }

    [HtmlAttributeName("asp-route-id")]
    public Guid ItemId { get; set; }

    // <summary>
    /// متن پیغام تایید حذف (پیغام پیش‌فرض: "آیا از حذف مطمئن هستید؟")
    /// </summary>
    [HtmlAttributeName("confirm-text")]
    public string ConfirmText { get; set; } = "آیا از حذف مطمئن هستید؟";

    /// <summary>
    /// عنوان پیغام تایید حذف (مثلاً "مطمئن هستید؟")
    /// </summary>
    [HtmlAttributeName("confirm-title")]
    public string ConfirmTitle { get; set; } = "مطمئن هستی؟";

    /// <summary>
    /// متن دکمه تایید حذف
    /// </summary>
    [HtmlAttributeName("confirm-button-text")]
    public string ConfirmButtonText { get; set; } = "بله حذف کنید!";


    /// <summary>
    /// متن دکمه لغو
    /// </summary>
    [HtmlAttributeName("cancel-button-text")]
    public string CancelButtonText { get; set; } = "انصراف";


    /// <summary>
    /// کلاس‌های css دلخواه برای دکمه (اگر null یا خالی باشد، مقدار پیش‌فرض استفاده می‌شود)
    /// </summary>
    [HtmlAttributeName("class")]
    public string Class { get; set; } = "btn btn-danger btn-sm";


    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        var urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);

        var url = urlHelper.Page(Page, PageHandler, new { id = ItemId });

        output.TagName = "button";
        output.TagMode = TagMode.StartTagAndEndTag;

        output.Attributes.SetAttribute("type", "button");

        var classes = string.IsNullOrWhiteSpace(Class) ? "btn btn-danger btn-sm" : Class;
        if (!classes.Contains("delete-item-btn"))
        {
            classes += " delete-item-btn";
        }
        output.Attributes.SetAttribute("class", classes.Trim());

        // داده‌های داده‌ای برای استفاده در JS
        output.Attributes.SetAttribute("data-url", url);
        output.Attributes.SetAttribute("data-confirm-text", ConfirmText);
        output.Attributes.SetAttribute("data-confirm-title", ConfirmTitle);
        output.Attributes.SetAttribute("data-confirm-button-text", ConfirmButtonText);
        output.Attributes.SetAttribute("data-cancel-button-text", CancelButtonText);

        // aria-label برای دسترس‌پذیری
        output.Attributes.SetAttribute("aria-label", ConfirmText);

        // محتویات داخلی (اگر کاربر خودش گذاشته باشد، حفظ می‌شود)
        if (!output.Content.IsModified)
        {
            output.Content.SetHtmlContent("<i class=\"ti ti-trash\"></i>");
        }
    }
}
