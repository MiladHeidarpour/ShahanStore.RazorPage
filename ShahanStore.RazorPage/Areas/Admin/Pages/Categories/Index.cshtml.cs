using Microsoft.AspNetCore.Mvc;
using ShahanStore.RazorPage.Infrastructure.RazorUtils;
using ShahanStore.RazorPage.Models.Bases;
using ShahanStore.RazorPage.Models.Commands.Categories;
using ShahanStore.RazorPage.Models.Queries.Categories;
using ShahanStore.RazorPage.Services.Categories;

namespace ShahanStore.RazorPage.Areas.Admin.Pages.Categories;

public class IndexModel : BaseRazorFilter<CategoryFilterParams>
{
    private readonly ICategoryService _categoryService;
    public IndexModel(ICategoryService categoryService)
    {
        _categoryService = categoryService;
    }


    [BindProperty(SupportsGet = true)]
    public int PageId { get; set; }

    [BindProperty(SupportsGet = true)]
    public int Take { get; set; } = 10;

    [BindProperty(SupportsGet = true)]
    public string? Search { get; set; }

    [BindProperty(SupportsGet = true)]
    public Status? Status { get; set; }

    //[BindProperty(SupportsGet = true)]
    //public string? Slug { get; set; }

    //[BindProperty(SupportsGet = true)]
    //public Guid? CategoryId { get; set; }



    public CategoryFilterResult FilterResult { get; set; }
    public async Task<IActionResult> OnGet()
    {
        FilterResult = await _categoryService.GetByFilter(FilterParams);
        return Page();
    }

    //public async Task<IActionResult> OnGetCategories()
    //{
    //    var filterResult = await _categoryService.GetByFilter(FilterParams);

    //    var categoryHtml = await this.RenderViewAsync("_CategoryList", filterResult.Data, true);
    //    var paginationHtml = await this.RenderViewAsync("_Pagination", filterResult, true);

    //    return Json(new { categoryHtml, paginationHtml });
    //}

    public async Task<IActionResult> OnGetEditPartial(Guid id)
    {
        var categoryDto = await _categoryService.GetById(id);
        if (categoryDto is null)
        {
            return NotFound(new { description = "دسته‌بندی مورد نظر یافت نشد." });
        }

        var modelForPartial = new EditCategoryDto()
        {
            Id = categoryDto.Id,
            Title = categoryDto.Title,
            Slug = categoryDto.Slug,
            IsDeleted = categoryDto.IsDeleted,
            SeoData = categoryDto.SeoData,
        };

        return Partial("_Edit", modelForPartial);
    }

    public async Task<IActionResult> OnPostEditPartial(EditCategoryDto command)
    {
        var result = await _categoryService.EditCategory(command);
        return RedirectToPageWithAlert(result.Message, result, "Index");
    }

    public async Task<IActionResult> OnGetAddChildPartial(Guid id)
    {
        var categoryDto = await _categoryService.GetById(id);
        if (categoryDto is null)
        {
            return NotFound(new { description = "دسته‌بندی مورد نظر یافت نشد." });
        }

        var modelForPartial = new AddChildCategoryDto()
        {
            ParentId = id,
            Title = null,
            Slug = null,
            BannerImg = null,
            Icon = null,
            SeoData = null,
        };

        return Partial("_AddChild", modelForPartial);
    }

    public async Task<IActionResult> OnPostAddChildPartial(AddChildCategoryDto command)
    {
        var result = await _categoryService.AddChildCategory(command);
        return RedirectToPageWithAlert(result.Message, result, "Index");
    }



    public async Task<IActionResult> OnGetChangeBannerPartial(Guid id)
    {
        var categoryDto = await _categoryService.GetById(id);
        if (categoryDto is null)
        {
            return NotFound(new { description = "دسته‌بندی مورد نظر یافت نشد." });
        }

        var modelForPartial = new ChangeCategoryBannerDto()
        {
            CategoryId = categoryDto.Id,
            BannerImg = null
        };

        return Partial("_ChangeCategoryBanner", modelForPartial);
    }
    public async Task<IActionResult> OnPostChangeBannerPartial(ChangeCategoryBannerDto command)
    {
        var result = await _categoryService.ChangeBanner(command);
        return RedirectToPageWithAlert(result.Message, result, "Index");
    }

    public async Task<IActionResult> OnGetChangeIconPartial(Guid id)
    {
        var categoryDto = await _categoryService.GetById(id);
        if (categoryDto is null)
        {
            return NotFound(new { description = "دسته‌بندی مورد نظر یافت نشد." });
        }

        var modelForPartial = new ChangeCategoryIconDto()
        {
            CategoryId = categoryDto.Id,
            Icon = null
        };

        return Partial("_ChangeCategoryIcon", modelForPartial);
    }
    public async Task<IActionResult> OnPostChangeIconPartial(ChangeCategoryIconDto command)
    {
        var result = await _categoryService.ChangeIcon(command);
        return RedirectToPageWithAlert(result.Message, result, "Index");
    }

    public async Task<IActionResult> OnPostDeletePartial(Guid id)
    {
        return await AjaxTryCatch(() => _categoryService.DeleteCategory(id),
       reloadOnSuccess: true,
       reloadOnError: false);
    }
}