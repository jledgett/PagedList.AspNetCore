namespace PagedList.AspNetCore;

public partial class PagedListRenderOptions
{
    private const string DefaultContainerHtmlTag = "nav";
    private static readonly string[] DefaultUlElementClasses = ["pagination"];
    private static readonly string[] DefaultLiElementClasses = ["page-item"];
    private static readonly string[] DefaultAhrefElementClasses = ["page-link"];
    private const string DefaultLinkToPreviousPageFormat = "Previous";
    private const string DefaultLinkToNextPageFormat = "Next";
    private const string DefaultLinkToFirstPageFormat = "First";
    private const string DefaultLinkToLastPageFormat = "Last";

    private static void SetBootstrap4Option(PagedListRenderOptions option)
    {
        option.ContainerHtmlTag = DefaultContainerHtmlTag;
        option.UlElementClasses = DefaultUlElementClasses;
        option.LiElementClasses = DefaultLiElementClasses;
        option.AhrefElementClasses = DefaultAhrefElementClasses;
        option.LinkToPreviousPageFormat = DefaultLinkToPreviousPageFormat;
        option.LinkToNextPageFormat = DefaultLinkToNextPageFormat;
        option.LinkToFirstPageFormat = DefaultLinkToFirstPageFormat;
        option.LinkToLastPageFormat = DefaultLinkToLastPageFormat;
    }

    /// <summary>Bootstrap 4/5 minimal style: Previous and Next links only.</summary>
    public static PagedListRenderOptions Bootstrap4Minimal
    {
        get { var o = new PagedListRenderOptions(); SetBootstrap4Option(o); SetMinimalOption(o); return o; }
    }

    /// <summary>Bootstrap 4/5 style: page numbers only.</summary>
    public static PagedListRenderOptions Bootstrap4PageNumbersOnly
    {
        get { var o = new PagedListRenderOptions(); SetBootstrap4Option(o); SetPageNumbersOnlyOption(o); return o; }
    }

    /// <summary>Bootstrap 4/5 style: page numbers plus Previous and Next.</summary>
    public static PagedListRenderOptions Bootstrap4PageNumbersPlusPrevAndNext
    {
        get { var o = new PagedListRenderOptions(); SetBootstrap4Option(o); SetPageNumbersPlusPrevAndNextOption(o); return o; }
    }

    /// <summary>Bootstrap 4/5 style: page numbers plus First and Last.</summary>
    public static PagedListRenderOptions Bootstrap4PageNumbersPlusFirstAndLast
    {
        get { var o = new PagedListRenderOptions(); SetBootstrap4Option(o); SetPageNumbersPlusFirstAndLastOption(o); return o; }
    }

    /// <summary>Bootstrap 4/5 full style: all navigation links.</summary>
    public static PagedListRenderOptions Bootstrap4Full
    {
        get { var o = new PagedListRenderOptions(); SetBootstrap4Option(o); SetFullOption(o); return o; }
    }
}
