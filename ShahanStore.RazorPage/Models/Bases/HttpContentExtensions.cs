using Microsoft.AspNetCore.Http;
using System.Collections;
using System.Reflection;

namespace ShahanStore.RazorPage.Models.Bases;

public static class HttpContentExtensions
{
    public static MultipartFormDataContent ToMultipartFormData<T>(this T obj) where T : class
    {
        var formData = new MultipartFormDataContent();
        AddPropertiesToFormData(obj, formData, "");
        return formData;
    }

    private static void AddPropertiesToFormData(object obj, MultipartFormDataContent formData, string prefix)
    {
        if (obj == null) return;

        var properties = obj.GetType().GetProperties();

        foreach (var property in properties)
        {
            var propertyName = $"{prefix}{property.Name}";
            var value = property.GetValue(obj);

            if (value == null) continue;

            // اگر پراپرتی از نوع فایل است
            if (value is IFormFile file)
            {
                var fileContent = new StreamContent(file.OpenReadStream());
                fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(file.ContentType);
                formData.Add(fileContent, name: propertyName, fileName: file.FileName);
            }
            // اگر پراپرتی یک آبجکت پیچیده است (اما نه رشته یا کالکشن)
            else if (property.PropertyType.IsClass && property.PropertyType != typeof(string) && !(value is IEnumerable))
            {
                // به صورت بازگشتی، پراپرتی‌های آبجکت تودرتو را اضافه می‌کنیم
                AddPropertiesToFormData(value, formData, $"{propertyName}.");
            }
            // برای انواع داده ساده
            else
            {
                formData.Add(new StringContent(value.ToString() ?? string.Empty), propertyName);
            }
        }
    }
}
