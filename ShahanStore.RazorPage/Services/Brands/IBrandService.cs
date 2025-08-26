using ShahanStore.RazorPage.Models.Bases;
using ShahanStore.RazorPage.Models.Commands.Brands;
using ShahanStore.RazorPage.Models.Queries.Brands;

namespace ShahanStore.RazorPage.Services.Brands;

public interface IBrandService
{
    #region Commands
    Task<ApiResult> CreateBrand(CreateBrandDto createDto);
    Task<ApiResult> EditBrand(EditBrandDto editDto);
    Task<ApiResult> DeleteBrand(Guid id);
    Task<ApiResult> ChangeBanner(ChangeBrandBannerDto changeBannerDto);
    Task<ApiResult> ChangeLogo(ChangeBrandLogoDto changeLogoDto);

    #endregion


    #region Queries
    Task<BrandFilterResult> GetByFilter(BrandFilterParams filterParams);
    Task<BrandDto> GetById(Guid id);
    Task<BrandDto> GetBySlug(string slug);

    #endregion
}
