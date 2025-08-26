using ShahanStore.RazorPage.Models.Bases;

namespace ShahanStore.RazorPage.Models.Queries.Brands;

public sealed record BrandFilterData(
    Guid Id,
    DateTimeOffset CreationDate,
    string Name,
    string Slug,
    string? Logo,
    bool IsAvailable) : BaseDto(Id, CreationDate);
