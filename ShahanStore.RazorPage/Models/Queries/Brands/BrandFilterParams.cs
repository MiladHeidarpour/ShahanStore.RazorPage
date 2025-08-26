using ShahanStore.RazorPage.Models.Bases;

namespace ShahanStore.RazorPage.Models.Queries.Brands;

public sealed record BrandFilterParams(
    string? Search,
    Status? Status,
    int PageId = 1,
    int Take = 10) : BaseFilterParam(PageId, Take);
