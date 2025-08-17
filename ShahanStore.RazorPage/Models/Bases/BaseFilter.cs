namespace ShahanStore.RazorPage.Models.Bases;

public class BaseFilter
{
    public int EntityCount { get; set; }
    public int CurrentPage { get; set; }
    public int PageCount { get; set; }
    public int StartPage { get; set; }
    public int EndPage { get; set; }
    public int Take { get; set; }
}

public class BaseFilter<TData, TParam> : BaseFilter where TParam : BaseFilterParam where TData : BaseDto
{
    public List<TData> Data { get; set; }
    public TParam FilterParams { get; set; }
}