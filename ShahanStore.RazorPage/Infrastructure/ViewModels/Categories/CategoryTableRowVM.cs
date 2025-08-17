using ShahanStore.RazorPage.Models.Bases;
using ShahanStore.RazorPage.Models.Queries.Categories;
using ShahanStore.RazorPage.Utilities.Categories;

namespace ShahanStore.RazorPage.Infrastructure.ViewModels.Categories;

public class CategoryTableRowVM : ICateoryTableRow
{
    public Guid Id { get; set; }
    public DateTimeOffset CreationDate { get; set; }
    public string Title { get; set; }
    public string Slug { get; set; }
    public Guid? ParentId { get; set; }
    public string? BannerImg { get; set; }
    public string? Icon { get; set; }
    public bool IsDeleted { get; set; }
    public SeoDataDto SeoData { get; set; }
    public List<ChildCategoryDto>? Children { get; set; }
    public List<CategoryAttributeDto>? Attributes { get; set; }

    public static CategoryTableRowVM FromCategoryDto(CategoryDto category)
    {
        return new CategoryTableRowVM()
        {
            Id = category.Id,
            CreationDate = category.CreationDate,
            Title = category.Title,
            Slug = category.Slug,
            ParentId = category.ParentId,
            BannerImg = category.BannerImg,
            Icon = category.Icon,
            IsDeleted = category.IsDeleted,
            SeoData = category.SeoData,
            Children = category.Children,
            Attributes = category.Attributes,
        };
    }

    public static CategoryTableRowVM FromChildCategoryDto(ChildCategoryDto category)
    {
        return new CategoryTableRowVM()
        {
            Id = category.Id,
            CreationDate = category.CreationDate,
            Title = category.Title,
            Slug = category.Slug,
            ParentId = category.ParentId,
            BannerImg = category.BannerImg,
            Icon = category.Icon,
            IsDeleted = category.IsDeleted,
            SeoData = category.SeoData,
            Attributes = category.Attributes,
        };
    }
}
