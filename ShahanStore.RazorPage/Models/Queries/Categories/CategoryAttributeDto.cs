using ShahanStore.RazorPage.Models.Bases;

namespace ShahanStore.RazorPage.Models.Queries.Categories;

public sealed record CategoryAttributeDto(
    Guid Id,
    DateTimeOffset CreationDate,
    string Name,
    List<string> PossibleValues
) : BaseDto(Id, CreationDate);