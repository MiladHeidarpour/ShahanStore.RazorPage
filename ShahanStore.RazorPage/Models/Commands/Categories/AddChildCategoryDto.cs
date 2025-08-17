using ShahanStore.RazorPage.Models.Bases;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ShahanStore.RazorPage.Models.Commands.Categories;

public sealed class AddChildCategoryDto
{
    [Required]
    public Guid ParentId { get; set; }

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