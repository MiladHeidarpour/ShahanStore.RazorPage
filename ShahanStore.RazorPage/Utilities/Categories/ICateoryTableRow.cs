using ShahanStore.RazorPage.Models.Bases;
using ShahanStore.RazorPage.Models.Queries.Categories;

namespace ShahanStore.RazorPage.Utilities.Categories;

public interface ICateoryTableRow
{
    public Guid Id { get; }
    public DateTimeOffset CreationDate { get; }
    public string Title { get; }
    public string Slug { get; }
    public Guid? ParentId { get; }
    public string? BannerImg { get; }
    public string? Icon { get; }
    public bool IsDeleted { get; }
    public SeoDataDto SeoData { get; }
    public List<ChildCategoryDto>? Children { get; }
    public List<CategoryAttributeDto>? Attributes { get; }
}