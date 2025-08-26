using Microsoft.AspNetCore.WebUtilities;
using ShahanStore.RazorPage.Models.Bases;
using ShahanStore.RazorPage.Models.Commands.Brands;
using ShahanStore.RazorPage.Models.Queries.Brands;

namespace ShahanStore.RazorPage.Services.Brands;

internal sealed class BrandService(HttpClient client, ILogger<BrandService> logger) : IBrandService
{

    #region Commands
    public async Task<ApiResult> CreateBrand(CreateBrandDto createDto)
    {
        using var formData = createDto.ToMultipartFormData();
        var response = await client.PostAsync("Brand", formData);
        return await response.ToApiResult();
    }

    public async Task<ApiResult> EditBrand(EditBrandDto editDto)
    {
        using var formData = editDto.ToMultipartFormData();
        var response = await client.PutAsync("Brand", formData);
        return await response.ToApiResult();
    }

    public async Task<ApiResult> DeleteBrand(Guid id)
    {
        var response = await client.DeleteAsync($"Brand/{id}");
        return await response.ToApiResult();
    }

    public async Task<ApiResult> ChangeBanner(ChangeBrandBannerDto changeBannerDto)
    {
        using var formData = changeBannerDto.ToMultipartFormData();
        var response = await client.PutAsync("Brand/ChangeBanner", formData);
        return await response.ToApiResult();
    }

    public async Task<ApiResult> ChangeLogo(ChangeBrandLogoDto changeLogoDto)
    {
        using var formData = changeLogoDto.ToMultipartFormData();
        var response = await client.PutAsync("Brand/ChangeLogo", formData);
        return await response.ToApiResult();
    }

    #endregion


    #region Queries
    public async Task<BrandFilterResult> GetByFilter(BrandFilterParams filterParams)
    {
        var queryParams = new Dictionary<string, string>();
        queryParams.Add("pageId", filterParams.PageId.ToString());
        queryParams.Add("take", filterParams.Take.ToString());


        if (!string.IsNullOrWhiteSpace(filterParams.Search))
            queryParams.Add("search", filterParams.Search);

        if (filterParams.Status != null)
            queryParams.Add("status", filterParams.Status.ToString());

        var url = QueryHelpers.AddQueryString("Brand/filter", queryParams);

        try
        {
            var result = await client.GetFromJsonAsync<ApiResult<BrandFilterResult>>(url);
            if (result == null || result.Data == null)
            {
                return new BrandFilterResult { Data = new List<BrandFilterData>() };
            }

            return result.Data;
        }
        catch (Exception ex)
        {

            logger.LogError(ex, "Failed to fetch Categories from API.");
            return new BrandFilterResult { Data = new List<BrandFilterData>() };
        }
    }

    public async Task<BrandDto> GetById(Guid id)
    {
        var result = await client.GetFromJsonAsync<ApiResult<BrandDto>>($"Brand/{id}");
        return result.Data;
    }

    public async Task<BrandDto> GetBySlug(string slug)
    {
        var result = await client.GetFromJsonAsync<ApiResult<BrandDto>>($"Brand/{slug}");
        return result.Data;
    }
    #endregion
} 
