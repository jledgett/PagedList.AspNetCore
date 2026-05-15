using Microsoft.AspNetCore.Mvc;
using PagedList.AspNetCore.Sample.Models;
using PagedList.AspNetCore.Sample.Search;
using PagedList.AspNetCore.Sample.Search.Services;

namespace PagedList.AspNetCore.Sample.Controllers;

public class SearchController : Controller
{
    private readonly ISearchService _searchService;

    public SearchController(ISearchService searchService)
    {
        _searchService = searchService;
    }

    public IActionResult Index(string? query, int? page)
    {
        var pageNumber = page is null or <= 0 ? 1 : page.Value;
        const int pageSize = 5;

        var model = new SearchViewModel
        {
            SearchResult = string.IsNullOrWhiteSpace(query)
                ? new SearchResult { SearchQuery = query }
                : _searchService.GetSearchResult(query, pageNumber, pageSize)
        };

        model.SearchResult.SearchQuery ??= query;
        return View(model);
    }
}
