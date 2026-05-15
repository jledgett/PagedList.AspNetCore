using PagedList;

namespace PagedList.AspNetCore.Sample.Search;

public class SearchResult
{
    public IPagedList<SearchHit>? SearchHits { get; set; }
    public string? SearchQuery { get; set; }
}
