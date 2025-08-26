using Microsoft.AspNetCore.Mvc;
using ShahanStore.RazorPage.Infrastructure.RazorUtils;
using ShahanStore.RazorPage.Models.Bases;
using ShahanStore.RazorPage.Models.Commands.Brands;
using ShahanStore.RazorPage.Models.Queries.Brands;
using ShahanStore.RazorPage.Services.Brands;

namespace ShahanStore.RazorPage.Areas.Admin.Pages.Brands;

public class IndexModel : BaseRazorFilter<BrandFilterParams>
{
    private readonly IBrandService _brandService;

    public IndexModel(IBrandService brandService)
    {
        _brandService = brandService;
    }

    [BindProperty(SupportsGet = true)]
    public int PageId { get; set; }

    [BindProperty(SupportsGet = true)]
    public int Take { get; set; } = 10;

    [BindProperty(SupportsGet = true)]
    public string? Search { get; set; }

    [BindProperty(SupportsGet = true)]
    public Status? Status { get; set; }


    public BrandFilterResult FilterResult { get; set; }
    public async Task<IActionResult> OnGet()
    {
        FilterResult = await _brandService.GetByFilter(FilterParams);
        return Page();
    }


    public async Task<IActionResult> OnGetEditPartial(Guid id)
    {
        var brand = await _brandService.GetById(id);
        if (brand is null)
        {
            return NotFound(new { description = "دسته‌بندی مورد نظر یافت نشد." });
        }
        var modelForPartial = new EditBrandDto()
        {
            Id = brand.Id,
            Name = brand.Name,
            Slug = brand.Slug,
            Description = brand.Description,
            IsAvailable = brand.IsAvailable,
            SeoData = brand.SeoData,
        };

        return Partial("_Edit", modelForPartial);
    }
    public async Task<IActionResult> OnPostEditPartial(EditBrandDto command)
    {
        var result = await _brandService.EditBrand(command);
        return RedirectToPageWithAlert(result.Message, result, "Index");
    }

    public async Task<IActionResult> OnGetChangeBannerPartial(Guid id)
    {
        var brandDto = await _brandService.GetById(id);
        if (brandDto is null)
        {
            return NotFound(new { description = "‌برند مورد نظر یافت نشد." });
        }

        var modelForPartial = new ChangeBrandBannerDto()
        {
            BrandId = brandDto.Id,
            BannerImg = null
        };

        return Partial("_ChangeBrandBanner", modelForPartial);
    }
    public async Task<IActionResult> OnPostChangeBannerPartial(ChangeBrandBannerDto command)
    {
        var result = await _brandService.ChangeBanner(command);
        return RedirectToPageWithAlert(result.Message, result, "Index");
    }

    public async Task<IActionResult> OnGetChangeLogoPartial(Guid id)
    {
        var brandDto = await _brandService.GetById(id);
        if (brandDto is null)
        {
            return NotFound(new { description = "برند مورد نظر یافت نشد." });
        }

        var modelForPartial = new ChangeBrandLogoDto()
        {
            BrandId = brandDto.Id,
            Logo = null
        };

        return Partial("_ChangeBrandLogo", modelForPartial);
    }
    public async Task<IActionResult> OnPostChangeLogoPartial(ChangeBrandLogoDto command)
    {
        var result = await _brandService.ChangeLogo(command);
        return RedirectToPageWithAlert(result.Message, result, "Index");
    }

    public async Task<IActionResult> OnPostDeletePartial(Guid id)
    {
        return await AjaxTryCatch(() => _brandService.DeleteBrand(id),
        reloadOnSuccess: true,
        reloadOnError: false);
    }
}
