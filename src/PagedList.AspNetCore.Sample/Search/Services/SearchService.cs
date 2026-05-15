using PagedList;

namespace PagedList.AspNetCore.Sample.Search.Services;

public class SearchService : ISearchService
{
    private static readonly List<Search.SearchHit> SampleData = Enumerable.Range(1, 500)
        .Select(i => new Search.SearchHit { Id = i, Title = $"PagedList Core Mvc - Search item {i}" })
        .ToList();

    public Search.SearchResult GetSearchResult(string query, int page, int pageSize)
    {
        var hits = SampleData
            .Where(x => x.Title.Contains(query, StringComparison.CurrentCultureIgnoreCase))
            .ToList();

        return new Search.SearchResult
        {
            SearchHits = new StaticPagedList<Search.SearchHit>(
                hits.Skip((page - 1) * pageSize).Take(pageSize),
                page, pageSize, hits.Count),
            SearchQuery = query
        };
    }
}
