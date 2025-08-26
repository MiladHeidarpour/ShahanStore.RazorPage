using System.ComponentModel.DataAnnotations;

namespace ShahanStore.RazorPage.Models.Commands.Brands;

public class ChangeBrandBannerDto
{
    [Required]
    public Guid BrandId { get; set; }


    [Display(Name = "بنر")]
    [Required(ErrorMessage = "بنر را وارد کنید!")]
    public IFormFile BannerImg { get; set; }
}
