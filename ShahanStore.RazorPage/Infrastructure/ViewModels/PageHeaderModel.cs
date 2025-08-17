namespace ShahanStore.RazorPage.Infrastructure.ViewModels;

public class PageHeaderModel
{
    public string Title { get; set; }
    public List<BreadcrumbItem> Breadcrumbs { get; set; } = new();
}

public class BreadcrumbItem
{
    public string Text { get; set; }
    public string Url { get; set; }
    public bool IsActive { get; set; }
}