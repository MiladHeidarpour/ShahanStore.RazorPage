using System.ComponentModel.DataAnnotations;

namespace ShahanStore.RazorPage.Models.Commands.Brands;

public class ChangeBrandLogoDto
{
    [Required]
    public Guid BrandId { get; set; }


    [Display(Name = "لوگو")]
    [Required(ErrorMessage = "لوگو را وارد کنید!")]
    public IFormFile Logo { get; set; }
}