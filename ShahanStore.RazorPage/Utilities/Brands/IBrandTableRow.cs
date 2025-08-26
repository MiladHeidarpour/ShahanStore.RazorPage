using ShahanStore.RazorPage.Models.Bases;

namespace ShahanStore.RazorPage.Utilities.Brands;

public interface IBrandTableRow
{
    public Guid Id { get; }
    public DateTimeOffset CreationDate { get; }
    public string Name { get; }
    public string Slug { get; }
    public string? BannerImg { get; }
    public string? Logo { get; }
    public string? Description { get; }
    public bool IsAvailable { get; }
    public SeoDataDto SeoData { get; }
}