using ShahanStore.RazorPage.Models.Bases;

namespace ShahanStore.RazorPage.Models.Queries.Categories;

public sealed record CategoryFilterParams(
    //Guid? CategoryId,
    //string? Slug,
    string? Search,
    Status? Status,
    int PageId = 1,
    int Take = 10) : BaseFilterParam(PageId, Take);

public enum Status
{
    Active,
    NotActive
}