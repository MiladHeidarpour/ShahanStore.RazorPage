using ShahanStore.RazorPage.Models.Bases;

namespace ShahanStore.RazorPage.Models.Queries.Brands;

public sealed record BrandDto(
    Guid Id,
    DateTimeOffset CreationDate,
    string Name,
    string Slug,
    string? BannerImg,
    string? Logo,
    string? Description,
    bool IsAvailable,
    SeoDataDto SeoData) : BaseDto(Id, CreationDate);

