using PagedList.AspNetCore;

namespace PagedList.AspNetCore.Sample.Pagination;

public static class SitePagedListRenderOptions
{
    public static PagedListRenderOptions Bootstrap4
    {
        get
        {
            var option = PagedListRenderOptions.Bootstrap4Full;
            option.MaximumPageNumbersToDisplay = 5;
            return option;
        }
    }
}
