using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.WebUtilities;
using ShahanStore.RazorPage.Models.Bases;
using ShahanStore.RazorPage.Models.Commands.Categories;
using ShahanStore.RazorPage.Models.Queries.Categories;

namespace ShahanStore.RazorPage.Services.Categories;

public class CategoryService(HttpClient client, ILogger<CategoryService> logger) : ICategoryService
{
    #region Commands
    public async Task<ApiResult> CreateCategory(CreateCategoryDto createDto)
    {
        using var formData = createDto.ToMultipartFormData();
        var response = await client.PostAsync("Category", formData);
        return await response.ToApiResult();
    }


    public async Task<ApiResult> AddChildCategory(AddChildCategoryDto addChildDto)
    {
        using var formData = addChildDto.ToMultipartFormData();
        var response = await client.PostAsync("Category/AddChild", formData);
        return await response.ToApiResult();
    }


    public async Task<ApiResult> EditCategory(EditCategoryDto editDto)
    {
        using var formData = editDto.ToMultipartFormData();
        var response = await client.PutAsync("Category", formData);
        return await response.ToApiResult();
    }


    public async Task<ApiResult> DeleteCategory(Guid id)
    {
        var result = await client.DeleteAsync($"Category/{id}");
        return await result.Content.ReadFromJsonAsync<ApiResult>();
    }


    public async Task<ApiResult> ChangeBanner(ChangeCategoryBannerDto changeBannerDto)
    {
        using var formData = changeBannerDto.ToMultipartFormData();
        var response = await client.PutAsync("Category/ChangeBanner", formData);
        return await response.ToApiResult();
    }


    public async Task<ApiResult> ChangeIcon(ChangeCategoryIconDto changeIconDto)
    {
        using var formData = changeIconDto.ToMultipartFormData();
        var response = await client.PutAsync("Category/ChangeIcon", formData);
        return await response.ToApiResult();
    }

    #endregion



    #region Queries

    public async Task<CategoryFilterResult> GetByFilter(CategoryFilterParams filterParams)
    {

        //var queryParams = new Dictionary<string, string?>
        //{
        //    ["pageId"] = filterParams.PageId.ToString(),
        //    ["take"] = filterParams.Take.ToString(),
        //    ["slug"] = filterParams.Slug,
        //    ["search"] = filterParams.Search,
        //    ["categoryId"] = filterParams.CategoryId?.ToString()
        //};
        var queryParams = new Dictionary<string, string>();
        queryParams.Add("pageId", filterParams.PageId.ToString());
        queryParams.Add("take", filterParams.Take.ToString());


        if (!string.IsNullOrWhiteSpace(filterParams.Search))
            queryParams.Add("search", filterParams.Search);

        if (filterParams.Status != null)
            queryParams.Add("status", filterParams.Status.ToString());

        //if (!string.IsNullOrWhiteSpace(filterParams.Slug))
        //    queryParams.Add("slug", filterParams.Slug);

        //if (filterParams.CategoryId.HasValue)
        //    queryParams.Add("categoryId", filterParams.CategoryId.ToString());

        var url = QueryHelpers.AddQueryString("category/filter", queryParams);

        try
        {
            var result = await client.GetFromJsonAsync<ApiResult<CategoryFilterResult>>(url);
            if (result == null || result.Data == null)
            {
                return new CategoryFilterResult { Data = new List<CategoryFilterData>() };
            }

            return result.Data;
        }
        catch (Exception ex)
        {

            logger.LogError(ex, "Failed to fetch Categories from API.");
            return new CategoryFilterResult { Data = new List<CategoryFilterData>() };
        }

        //var url = $"category/filter?pageId={filterParams.PageId}&take={filterParams.Take}" +
        //          $"&slug={filterParams.Slug}&search={filterParams.Search}&status={filterParams.Status}";
        //if (filterParams.CategoryId != null)
        //    url += $"&Id={filterParams.CategoryId}";
        //var result = await client.GetFromJsonAsync<ApiResult<CategoryFilterResult>>(url);
        //return result?.Data;
    }


    public async Task<CategoryDto> GetById(Guid id)
    {
        var result = await client.GetFromJsonAsync<ApiResult<CategoryDto>>($"Category/{id}");
        return result.Data;
    }


    public async Task<CategoryDto> GetByIdWithDetails(Guid id)
    {
        var result = await client.GetFromJsonAsync<ApiResult<CategoryDto>>($"Category/GetByIdWithDetails/{id}");
        return result.Data;
    }

    public async Task<CategoryDto> GetBySlug(string slug)
    {
        var result = await client.GetFromJsonAsync<ApiResult<CategoryDto>>($"Category/{slug}");
        return result.Data;
    }

    #endregion
}