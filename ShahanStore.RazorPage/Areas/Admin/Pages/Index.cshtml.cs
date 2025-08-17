using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShahanStore.RazorPage.Areas.Admin.Pages.Categories;
using ShahanStore.RazorPage.Infrastructure.RazorUtils;
using ShahanStore.RazorPage.Infrastructure.RazorUtils.RenderView;
using ShahanStore.RazorPage.Models.Bases;
using ShahanStore.RazorPage.Models.Commands.Categories;
using ShahanStore.RazorPage.Services.Categories;
using System.ComponentModel.DataAnnotations;

namespace ShahanStore.RazorPage.Areas.Admin.Pages;

public class IndexModel(ICategoryService categoryService, IRenderViewToString renderService) : BasePageModel
{

    [BindProperty]
    public InputModel Input { get; set; }

    public class InputModel
    {
        public Guid Id { get; set; }

        [Display(Name = "عنوان")]
        [Required(ErrorMessage = "عنوان را وارد کنید!")]
        public string Title { get; set; }

        [Display(Name = "اسلاگ")]
        [Required(ErrorMessage = "اسلاگ را وارد کنید!")]
        public string Slug { get; set; }

        [Display(Name = "سئودیتا")] public SeoDataDto SeoData { get; set; }
    }

    public void OnGet()
    {

    }
}