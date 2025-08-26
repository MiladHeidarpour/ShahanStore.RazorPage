using Microsoft.AspNetCore.Mvc;
using ShahanStore.RazorPage.Infrastructure.RazorUtils;
using ShahanStore.RazorPage.Models.Bases;
using ShahanStore.RazorPage.Models.Commands.Brands;
using ShahanStore.RazorPage.Services.Brands;
using System.ComponentModel.DataAnnotations;

namespace ShahanStore.RazorPage.Areas.Admin.Pages.Brands;

public class CreateModel(IBrandService brandService) : BasePageModel
{
    [BindProperty]
    public InputModel Input { get; set; }

    public class InputModel
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "عنوان را وارد کنید!")]
        public string Name { get; set; }

        [Display(Name = "اسلاگ")]
        [Required(ErrorMessage = "اسلاگ را وارد کنید!")]
        public string Slug { get; set; }

        [Display(Name = "بنر")]
        public IFormFile? BannerImg { get; set; }

        [Display(Name = "لوگو")]
        public IFormFile? Logo { get; set; }

        [Display(Name = "توضیحات")]
        public string? Description { get; set; }

        public SeoDataDto SeoData { get; set; }
    }
    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            ShowAlert("لطفاً تمام فیلدهای الزامی را پر کنید.", "Error");
            return Page();
        }
        var command = new CreateBrandDto()
        {
            Name = Input.Name,
            Slug = Input.Slug,
            BannerImg = Input.BannerImg,
            Logo = Input.Logo,
            Description = Input.Description,
            SeoData = Input.SeoData
        };

        var result = await brandService.CreateBrand(command);
        if (result.IsSuccess)
        {
            return RedirectToPageWithAlert(result.Message, result, "Index");
        }

        ShowAlert(result.Message, "Error");
        return Page();
    }
}
