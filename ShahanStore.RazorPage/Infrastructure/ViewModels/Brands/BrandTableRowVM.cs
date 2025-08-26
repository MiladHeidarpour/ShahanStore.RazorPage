using ShahanStore.RazorPage.Models.Bases;
using ShahanStore.RazorPage.Models.Queries.Brands;
using ShahanStore.RazorPage.Utilities.Brands;

namespace ShahanStore.RazorPage.Infrastructure.ViewModels.Brands;

public class BrandTableRowVM : IBrandTableRow
{
    public Guid Id { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public string Name { get; set; }
    public string Slug { get; set; }
    public string? BannerImg { get; set; }
    public string? Logo { get; set; }
    public string? Description { get; set; }
    public bool IsAvailable { get; set; }
    public SeoDataDto SeoData { get; set; }

    public static BrandTableRowVM FromBrandDto(BrandDto brand)
    {
        return new BrandTableRowVM()
        {
            Id = brand.Id,
            CreationDate = brand.CreationDate,
            Name = brand.Name,
            Slug = brand.Slug,
            BannerImg = brand.BannerImg,
            Logo = brand.Logo,
            Description= brand.Description,
            IsAvailable = brand.IsAvailable,
            SeoData = brand.SeoData,
        };
    }
}
