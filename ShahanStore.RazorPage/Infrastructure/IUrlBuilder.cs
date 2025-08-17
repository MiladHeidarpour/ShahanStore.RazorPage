using Microsoft.Extensions.Options;

namespace ShahanStore.RazorPage.Infrastructure;

public interface IUrlBuilder
{
    string BuildApiUrl(string relativePath);
}
public class UrlBuilder : IUrlBuilder
{
    private readonly ApiService _apiService;

    public UrlBuilder(IOptions<ApiService> apiSettings)
    {
        _apiService = apiSettings.Value;
    }

    public string BuildApiUrl(string relativePath)
    {
        var baseUrl = _apiService.BaseUrl.Replace("api","").TrimEnd('/');
        var relative = relativePath.TrimStart('/');
        return $"{baseUrl}/{relative}";
    }
}
public class ApiService
{
    public string BaseUrl { get; set; }
}
