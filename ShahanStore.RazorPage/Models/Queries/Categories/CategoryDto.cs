using ShahanStore.RazorPage.Models.Bases;

namespace ShahanStore.RazorPage.Models.Queries.Categories;

public sealed record CategoryDto(
    Guid Id,
    DateTimeOffset CreationDate,
    string Title,
    string Slug,
    Guid? ParentId,
    string? BannerImg,
    string? Icon,
    bool IsDeleted,
    SeoDataDto SeoData,
    List<ChildCategoryDto> Children,
    List<CategoryAttributeDto> Attributes) : BaseDto(Id, CreationDate);


public sealed record ChildCategoryDto(
    Guid Id,
    DateTimeOffset CreationDate,
    string Title,
    string Slug,
    Guid? ParentId,
    string? BannerImg,
    string? Icon,
    bool IsDeleted,
    SeoDataDto SeoData,
    List<CategoryAttributeDto> Attributes) : BaseDto(Id, CreationDate);