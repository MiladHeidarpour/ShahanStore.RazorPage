using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using ShahanStore.RazorPage.Infrastructure.RazorUtils;
using ShahanStore.RazorPage.Models.Bases;
using ShahanStore.RazorPage.Models.Commands.Categories;
using ShahanStore.RazorPage.Services.Categories;

namespace ShahanStore.RazorPage.Areas.Admin.Pages.Categories;

public class CreateModel(ICategoryService categoryService) : BasePageModel
{
    [BindProperty]
    public InputModel Input { get; set; }

    public class InputModel
    {
        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "عنوان را وارد کنید!")]
        public string Title { get; set; }

        [Display(Name = "اسلاگ")]
        [Required(ErrorMessage = "اسلاگ را وارد کنید!")]
        public string Slug { get; set; }

        [Display(Name = "بنر")]
        public IFormFile? BannerImg { get; set; }

        [Display(Name = "آیکون")]
        public IFormFile? Icon { get; set; }

        public SeoDataDto SeoData { get; set; }
    }
    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPost()
    {
        if (!ModelState.IsValid)
        {
            ShowAlert("لطفاً تمام فیلدهای الزامی را پر کنید.", "Warning");
            return Page();
        }
        var command = new CreateCategoryDto()
        {
            Title = Input.Title,
            Slug = Input.Slug,
            BannerImg = Input.BannerImg,
            Icon = Input.Icon,
            SeoData = Input.SeoData
        };

        var result = await categoryService.CreateCategory(command);
        if (result.IsSuccess)
        {
            return RedirectToPageWithAlert(result.Message, result, "Index");
        }

        ShowAlert(result.Message, "Error");
        return Page();
    }
}