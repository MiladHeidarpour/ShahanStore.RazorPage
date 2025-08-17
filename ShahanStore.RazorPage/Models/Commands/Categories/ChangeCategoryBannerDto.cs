using System.ComponentModel.DataAnnotations;

namespace ShahanStore.RazorPage.Models.Commands.Categories;

public class ChangeCategoryBannerDto
{
    [Required]
    public Guid CategoryId { get; set; }


    [Display(Name ="بنر")]
    [Required(ErrorMessage = "بنر را وارد کنید!")]
    public IFormFile BannerImg { get; set; }
}
