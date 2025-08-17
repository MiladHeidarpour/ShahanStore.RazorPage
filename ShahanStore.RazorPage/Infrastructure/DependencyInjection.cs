using ShahanStore.RazorPage.Services.Categories;
using System.Configuration;

namespace ShahanStore.RazorPage.Infrastructure;

public static class DependencyInjection
{
    const string baseAddress = "https://localhost:7040/api/";
    public static IServiceCollection AddUIDependency(this IServiceCollection services, IConfiguration configuration)
    {
        
        services.AddHttpClient<ICategoryService, CategoryService>(httpClient =>
        {
            string? baseAddress = configuration.GetValue<string>("ApiService:BaseUrl");
            httpClient.BaseAddress = new Uri(baseAddress!);
        });
        //.AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>(); // این خط برای احراز هویت است، بعداً می‌توانیم اضافه کنیم

        services.Configure<ApiService>(configuration.GetSection("ApiService"));
        services.AddScoped<IUrlBuilder, UrlBuilder>();

        return services;
    }
    
}