namespace ShahanStore.RazorPage.Models.Bases;

public record BaseFilterParam(
    int PageId = 1,
    int Take = 10
);