using Microsoft.AspNetCore.Http;
using ShahanStore.RazorPage.Models.Bases;
using ShahanStore.RazorPage.Models.Commands.Categories;
using ShahanStore.RazorPage.Models.Queries.Categories;

namespace ShahanStore.RazorPage.Services.Categories;

public interface ICategoryService
{
    #region Commands
    Task<ApiResult> CreateCategory(CreateCategoryDto createDto);
    Task<ApiResult> AddChildCategory(AddChildCategoryDto addChildDto);
    Task<ApiResult> EditCategory(EditCategoryDto editDto);
    Task<ApiResult> DeleteCategory(Guid id);
    Task<ApiResult> ChangeBanner(ChangeCategoryBannerDto changeBannerDto);
    Task<ApiResult> ChangeIcon(ChangeCategoryIconDto changeIconDto);

    #endregion


    #region Queries
    Task<CategoryFilterResult> GetByFilter(CategoryFilterParams filterParams);
    Task<CategoryDto> GetById(Guid id);
    Task<CategoryDto> GetByIdWithDetails(Guid id);
    Task<CategoryDto> GetBySlug(string slug);

    #endregion
}