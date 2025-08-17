using System.ComponentModel.DataAnnotations;

namespace ShahanStore.RazorPage.Models.Commands.Categories;

public class ChangeCategoryIconDto
{
    [Required]
    public Guid CategoryId { get; set; }


    [Display(Name = "آیکون")]
    [Required(ErrorMessage = "آیکون را وارد کنید!")]
    public IFormFile Icon { get; set; }
}
