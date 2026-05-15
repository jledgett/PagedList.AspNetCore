namespace PagedList.AspNetCore;

public partial class PagedListRenderOptions
{
    private static void SetMinimalOption(PagedListRenderOptions option)
    {
        option.DisplayLinkToFirstPage = PagedListDisplayMode.Never;
        option.DisplayLinkToLastPage = PagedListDisplayMode.Never;
        option.DisplayLinkToPreviousPage = PagedListDisplayMode.Always;
        option.DisplayLinkToNextPage = PagedListDisplayMode.Always;
        option.DisplayLinkToIndividualPages = false;
    }

    private static void SetPageNumbersOnlyOption(PagedListRenderOptions option)
    {
        option.DisplayLinkToFirstPage = PagedListDisplayMode.Never;
        option.DisplayLinkToLastPage = PagedListDisplayMode.Never;
        option.DisplayLinkToPreviousPage = PagedListDisplayMode.Never;
        option.DisplayLinkToNextPage = PagedListDisplayMode.Never;
        option.DisplayLinkToIndividualPages = true;
    }

    private static void SetPageNumbersPlusPrevAndNextOption(PagedListRenderOptions option)
    {
        option.DisplayLinkToFirstPage = PagedListDisplayMode.Never;
        option.DisplayLinkToLastPage = PagedListDisplayMode.Never;
        option.DisplayLinkToPreviousPage = PagedListDisplayMode.Always;
        option.DisplayLinkToNextPage = PagedListDisplayMode.Always;
        option.DisplayLinkToIndividualPages = true;
    }

    private static void SetPageNumbersPlusFirstAndLastOption(PagedListRenderOptions option)
    {
        option.DisplayLinkToFirstPage = PagedListDisplayMode.Always;
        option.DisplayLinkToLastPage = PagedListDisplayMode.Always;
        option.DisplayLinkToPreviousPage = PagedListDisplayMode.Never;
        option.DisplayLinkToNextPage = PagedListDisplayMode.Never;
        option.DisplayLinkToIndividualPages = true;
    }

    private static void SetFullOption(PagedListRenderOptions option)
    {
        option.DisplayLinkToFirstPage = PagedListDisplayMode.Always;
        option.DisplayLinkToLastPage = PagedListDisplayMode.Always;
        option.DisplayLinkToPreviousPage = PagedListDisplayMode.Always;
        option.DisplayLinkToNextPage = PagedListDisplayMode.Always;
        option.DisplayLinkToIndividualPages = true;
    }

    /// <summary>Previous and Next links only.</summary>
    public static PagedListRenderOptions Minimal
    {
        get { var o = new PagedListRenderOptions(); SetMinimalOption(o); return o; }
    }

    /// <summary>Page numbers only.</summary>
    public static PagedListRenderOptions PageNumbersOnly
    {
        get { var o = new PagedListRenderOptions(); SetPageNumbersOnlyOption(o); return o; }
    }

    /// <summary>Page numbers plus Previous and Next.</summary>
    public static PagedListRenderOptions PageNumbersPlusPrevAndNext
    {
        get { var o = new PagedListRenderOptions(); SetPageNumbersPlusPrevAndNextOption(o); return o; }
    }

    /// <summary>Page numbers plus First and Last.</summary>
    public static PagedListRenderOptions PageNumbersPlusFirstAndLast
    {
        get { var o = new PagedListRenderOptions(); SetPageNumbersPlusFirstAndLastOption(o); return o; }
    }

    /// <summary>All navigation links.</summary>
    public static PagedListRenderOptions Full
    {
        get { var o = new PagedListRenderOptions(); SetFullOption(o); return o; }
    }
}
