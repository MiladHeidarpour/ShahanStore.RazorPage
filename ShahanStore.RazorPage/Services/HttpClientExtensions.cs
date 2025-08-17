using ShahanStore.RazorPage.Models.Bases;
using System.Text.Json;

namespace ShahanStore.RazorPage.Services;

public static class HttpClientExtensions
{
    public static async Task<ApiResult> ToApiResult(this HttpResponseMessage response)
    {
        var options = new JsonSerializerOptions { PropertyNameCaseInsensitive = true };
        try
        {
            return await response.Content.ReadFromJsonAsync<ApiResult>(options)
                   ?? ApiResult.Error("پاسخ نامعتبر از سرور دریافت شد.");
        }
        catch
        {
            // اگر بدنه پاسخ قابل خواندن نبود (مثلاً خطای سرور 500)
            return ApiResult.Error("خطایی در ارتباط با سرور رخ داده است.");
        }
    }
}