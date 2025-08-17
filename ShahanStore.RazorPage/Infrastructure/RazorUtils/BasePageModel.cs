using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;
using ShahanStore.RazorPage.Models.Bases;
using System.Text.Json;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace ShahanStore.RazorPage.Infrastructure.RazorUtils;

[ValidateAntiForgeryToken]
public abstract class BasePageModel : PageModel
{
    public const string AlertKey = "SystemAlert";

    protected void ShowAlert(string message, string type = "Success")
    {
        TempData[AlertKey] = JsonSerializer.Serialize(new { Message = message, Type = type });
    }
    protected IActionResult RedirectToPageWithAlert(string? message, ApiResult result, string pageName)
    {
        //ShowAlert(result.Message, result.IsSuccess ? "Success" : "Error");
        ShowAlert(message ?? result.Message, result.IsSuccess ? "Success" : "Error");

        return RedirectToPage(pageName);
    }

    protected async Task<ContentResult> AjaxTryCatch(
        Func<Task<ApiResult>> func,
        bool reloadOnSuccess = false,
        bool reloadOnError = false)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                var errors = string.Join(" - ", ModelState.Values
                    .SelectMany(v => v.Errors)
                    .Select(e => e.ErrorMessage));
                var errorResult = new AjaxResponse
                {
                    Status = "BadRequest",
                    Message = errors,
                    ReloadPage = reloadOnError
                };
                return Content(JsonSerializer.Serialize(errorResult), "application/json");
            }

            var result = await func();

            var response = new AjaxResponse
            {
                Status = result.IsSuccess ? "Success" : "Error",
                Message = result.Message,
                ReloadPage = result.IsSuccess ? reloadOnSuccess : reloadOnError
            };

            return Content(JsonSerializer.Serialize(response), "application/json");
        }
        catch (Exception ex)
        {
            var errorResponse = new AjaxResponse
            {
                Status = "Error",
                Message = ex.Message,
                ReloadPage = reloadOnError
            };
            return Content(JsonSerializer.Serialize(errorResponse), "application/json");
        }
    }

    protected class AjaxResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public bool ReloadPage { get; set; }
    }
}