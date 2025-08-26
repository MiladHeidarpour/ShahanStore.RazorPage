using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShahanStore.RazorPage.Infrastructure.RazorUtils;
using ShahanStore.RazorPage.Models.Bases;
using ShahanStore.RazorPage.Models.Queries.Categories;
using ShahanStore.RazorPage.Services.Categories;

namespace ShahanStore.RazorPage.Areas.Admin.Pages.Categories;

public class DetailModel(ICategoryService categoryService) :  BaseRazorFilter<CategoryFilterParams>
{
    [BindProperty(SupportsGet = true)]
    public int PageId { get; set; }

    [BindProperty(SupportsGet = true)]
    public int Take { get; set; } = 10;

    [BindProperty(SupportsGet = true)]
    public string? Search { get; set; }

    [BindProperty(SupportsGet = true)]
    public Status? Status { get; set; }
    public CategoryFilterResult FilterResult { get; set; }

    public CategoryDto? Category;
    public async Task<IActionResult> OnGet(Guid id)
    {
        Category = await categoryService.GetByIdWithDetails(id);
        FilterResult= await categoryService.GetByFilter(FilterParams);

        if (Category is null)
        {
            return NotFound(new { description = "دسته‌بندی مورد نظر یافت نشد." });
        }
        return Page();
    }
}
