using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ShahanStore.RazorPage.Infrastructure.RazorUtils.RenderView;

public interface IRenderViewToString
{
    Task<string> RenderToStringAsync(string viewName, object model, PageContext context);
}