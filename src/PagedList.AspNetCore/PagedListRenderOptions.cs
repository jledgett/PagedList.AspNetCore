using PagedList;

namespace PagedList.AspNetCore;

/// <summary>
/// Options for configuring the output of the pager tag helper.
/// </summary>
public partial class PagedListRenderOptions
{
    /// <summary>
    /// The default settings render Bootstrap 4/5-compatible navigation links with no descriptive text.
    /// </summary>
    public PagedListRenderOptions()
    {
        DisplayLinkToFirstPage = PagedListDisplayMode.IfNeeded;
        DisplayLinkToLastPage = PagedListDisplayMode.IfNeeded;
        DisplayLinkToPreviousPage = PagedListDisplayMode.IfNeeded;
        DisplayLinkToNextPage = PagedListDisplayMode.IfNeeded;
        DisplayLinkToIndividualPages = true;
        DisplayPageCountAndCurrentLocation = false;
        MaximumPageNumbersToDisplay = 10;
        DisplayEllipsesWhenNotShowingAllPageNumbers = true;
        EllipsesFormat = "&#8230;";
        LinkToFirstPageFormat = "&laquo;";
        LinkToPreviousPageFormat = "&lsaquo;";
        LinkToIndividualPageFormat = "{0}";
        LinkToNextPageFormat = "&rsaquo;";
        LinkToLastPageFormat = "&raquo;";
        PageCountAndCurrentLocationFormat = "Page {0} of {1}.";
        ItemSliceAndTotalFormat = "Showing items {0} through {1} of {2}.";
        ClassToApplyToFirstListItemInPager = null;
        ClassToApplyToLastListItemInPager = null;
        ContainerHtmlTag = "nav";
        UlElementClasses = ["pagination"];
        LiElementClasses = [];
        AhrefElementClasses = [];
        ActiveElementClasses = ["active"];
        DisabledElementClasses = ["disabled"];
    }

    /// <summary>The HTML tag wrapping the ul element (default: nav).</summary>
    public string ContainerHtmlTag { get; set; }

    /// <summary>CSS classes to append to the &lt;ul&gt; element.</summary>
    public IEnumerable<string> UlElementClasses { get; set; }

    /// <summary>CSS classes to append to every &lt;li&gt; element.</summary>
    public IEnumerable<string> LiElementClasses { get; set; }

    /// <summary>CSS classes to append to every &lt;a&gt; element.</summary>
    public IEnumerable<string> AhrefElementClasses { get; set; }

    /// <summary>CSS classes applied to the active page list item.</summary>
    public IEnumerable<string> ActiveElementClasses { get; set; }

    /// <summary>CSS classes applied to disabled list items.</summary>
    public IEnumerable<string> DisabledElementClasses { get; set; }

    /// <summary>CSS class appended to the first list item. Null means no extra class.</summary>
    public string? ClassToApplyToFirstListItemInPager { get; set; }

    /// <summary>CSS class appended to the last list item. Null means no extra class.</summary>
    public string? ClassToApplyToLastListItemInPager { get; set; }

    /// <summary>Controls overall pager visibility.</summary>
    public PagedListDisplayMode Display { get; set; }

    /// <summary>Controls the First-page link.</summary>
    public PagedListDisplayMode DisplayLinkToFirstPage { get; set; }

    /// <summary>Controls the Last-page link.</summary>
    public PagedListDisplayMode DisplayLinkToLastPage { get; set; }

    /// <summary>Controls the Previous-page link.</summary>
    public PagedListDisplayMode DisplayLinkToPreviousPage { get; set; }

    /// <summary>Controls the Next-page link.</summary>
    public PagedListDisplayMode DisplayLinkToNextPage { get; set; }

    /// <summary>When true, renders a hyperlink for each individual page number.</summary>
    public bool DisplayLinkToIndividualPages { get; set; }

    /// <summary>When true, renders current-page and total-page count text.</summary>
    public bool DisplayPageCountAndCurrentLocation { get; set; }

    /// <summary>When true, renders first/last item index and total count text.</summary>
    public bool DisplayItemSliceAndTotal { get; set; }

    /// <summary>Maximum number of page number links to show. Null shows all.</summary>
    public int? MaximumPageNumbersToDisplay { get; set; }

    /// <summary>When true, inserts an ellipsis where page numbers are omitted.</summary>
    public bool DisplayEllipsesWhenNotShowingAllPageNumbers { get; set; }

    /// <summary>HTML to display for the ellipsis (default: &amp;#8230;).</summary>
    public string EllipsesFormat { get; set; }

    /// <summary>Text/HTML inside the link to the first page.</summary>
    public string LinkToFirstPageFormat { get; set; }

    /// <summary>Text/HTML inside the link to the previous page.</summary>
    public string LinkToPreviousPageFormat { get; set; }

    /// <summary>Text/HTML inside individual page number links. Use {0} for the page number.</summary>
    public string LinkToIndividualPageFormat { get; set; }

    /// <summary>Text/HTML inside the link to the next page.</summary>
    public string LinkToNextPageFormat { get; set; }

    /// <summary>Text/HTML inside the link to the last page.</summary>
    public string LinkToLastPageFormat { get; set; }

    /// <summary>Format for page-count text. {0} = current page, {1} = total pages.</summary>
    public string PageCountAndCurrentLocationFormat { get; set; }

    /// <summary>Format for item-slice text. {0} = first item, {1} = last item, {2} = total.</summary>
    public string ItemSliceAndTotalFormat { get; set; }

    /// <summary>Optional delimiter rendered between individual page number links.</summary>
    public string? DelimiterBetweenPageNumbers { get; set; }
}
