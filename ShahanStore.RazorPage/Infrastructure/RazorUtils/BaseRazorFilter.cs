using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ShahanStore.RazorPage.Models.Bases;

namespace ShahanStore.RazorPage.Infrastructure.RazorUtils;

public class BaseRazorFilter<TFilterParam> : BasePageModel where TFilterParam : BaseFilterParam
{
    [BindProperty(SupportsGet = true)]
    public TFilterParam FilterParams { get; set; }
}