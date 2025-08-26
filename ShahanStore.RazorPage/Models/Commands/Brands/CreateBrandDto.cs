using ShahanStore.RazorPage.Models.Bases;
using System.ComponentModel.DataAnnotations;

namespace ShahanStore.RazorPage.Models.Commands.Brands;

public class CreateBrandDto
{
    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "عنوان را وارد کنید!")]
    public string Name { get; set; }


    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "عنوان را وارد کنید!")]
    public string Slug { get; set; }


    [Display(Name = "بنر")]
    public IFormFile? BannerImg { get; set; }


    [Display(Name = "لوگو")]
    public IFormFile? Logo { get; set; }


    [Display(Name = "توضیحات")]
    public string? Description { get; set; }


    public SeoDataDto SeoData { get; set; }
}
