using PagedList.AspNetCore.Sample.Search;

namespace PagedList.AspNetCore.Sample.Models;

public class SearchViewModel
{
    public SearchResult SearchResult { get; set; } = new();
}
