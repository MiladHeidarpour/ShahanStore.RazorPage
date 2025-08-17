using System.ComponentModel.DataAnnotations;

namespace ShahanStore.RazorPage.Models.Bases;

public class SeoDataDto()
{
    // Meta Tags
    public string? MetaTitle { get; set; }
    public string? MetaDescription { get; set; }

    // Indexing and Canonical
    public bool IndexPage { get; set; }
    public string? Canonical { get; set; }

    // Social & Rich Snippets
    public string? OgTitle { get; set; }
    public string? OgDescription { get; set; }
    public string? OgImage { get; set; } // آدرس کامل تصویر
    public string? Schema { get; set; }
}
