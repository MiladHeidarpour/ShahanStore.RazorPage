using ShahanStore.RazorPage.Models.Bases;
using System.ComponentModel.DataAnnotations;

namespace ShahanStore.RazorPage.Models.Commands.Categories;

public class EditCategoryDto
{
    [Required]
    public Guid Id { get; set; }

    [Display(Name = "عنوان")]
    [Required(ErrorMessage = "عنوان را وارد کنید!")]
    public string Title { get; set; }

    [Display(Name = "اسلاگ")]
    [Required(ErrorMessage = "اسلاگ را وارد کنید!")]
    public string Slug { get; set; }

    [Display(Name = "غیرفعال")]
    public bool IsDeleted { get; set; }
    public SeoDataDto SeoData { get; set; }
}
