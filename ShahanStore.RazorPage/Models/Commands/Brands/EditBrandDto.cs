using ShahanStore.RazorPage.Models.Bases;
using System.ComponentModel.DataAnnotations;

namespace ShahanStore.RazorPage.Models.Commands.Brands;

public class EditBrandDto
{

    [Required]
    public Guid Id { get; set; }

    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "عنوان را وارد کنید!")]
    public string Name { get; set; }


    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "عنوان را وارد کنید!")]
    public string Slug { get; set; }


    [Display(Name = "توضیحات")]
    public string? Description { get; set; }


    [Display(Name = "فعال")]
    public bool IsAvailable { get; set; }

    public SeoDataDto SeoData { get; set; }
}
