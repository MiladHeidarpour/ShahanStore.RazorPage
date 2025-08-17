using ShahanStore.RazorPage.Models.Bases;

namespace ShahanStore.RazorPage.Models.Queries.Categories;

//public record CategoryFilterData(
//    Guid Id,
//    DateTimeOffset CreationDate,
//    string Title,
//    string Slug,
//    Guid? ParentId,
//    string? BannerImg,
//    string? Icon,
//    bool IsDeleted,
//    SeoDataDto SeoData,
//    List<CategoryAttributeDto> Attributes) : BaseDto(Id, CreationDate);

public record CategoryFilterData(
    Guid Id,
    DateTimeOffset CreationDate,
    string Title,
    string Slug,
    string? Icon,
    bool IsDeleted) : BaseDto(Id, CreationDate);