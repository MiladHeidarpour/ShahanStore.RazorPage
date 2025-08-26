using Microsoft.AspNetCore.Mvc;
using ShahanStore.RazorPage.Infrastructure.RazorUtils;
using ShahanStore.RazorPage.Models.Bases;
using ShahanStore.RazorPage.Models.Queries.Brands;
using ShahanStore.RazorPage.Services.Brands;


namespace ShahanStore.RazorPage.Areas.Admin.Pages.Brands;

public class DetailModel(IBrandService brandService) : BasePageModel
{
    [BindProperty(SupportsGet = true)]
    public int PageId { get; set; }

    [BindProperty(SupportsGet = true)]
    public int Take { get; set; } = 10;

    [BindProperty(SupportsGet = true)]
    public string? Search { get; set; }

    [BindProperty(SupportsGet = true)]
    public Status? Status { get; set; }

    public BrandDto? Brand;
    public async Task<IActionResult> OnGet(Guid id)
    {
        Brand = await brandService.GetById(id);

        if (Brand is null)
        {
            return NotFound(new { description = "برند مورد نظر یافت نشد." });
        }
        return Page();
    }
}
