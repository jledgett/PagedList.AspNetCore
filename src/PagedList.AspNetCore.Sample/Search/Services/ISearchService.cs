namespace PagedList.AspNetCore.Sample.Search.Services;

public interface ISearchService
{
    Search.SearchResult GetSearchResult(string query, int page, int pageSize);
}
