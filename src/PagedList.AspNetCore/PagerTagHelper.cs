using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.AspNetCore.Routing;
using PagedList;

namespace PagedList.AspNetCore;

[HtmlTargetElement("pager")]
public class PagerTagHelper : TagHelper
{
    private const string ListAttributeName = "list";
    [HtmlAttributeName(ListAttributeName)]
    public IPagedList? List { get; set; }

    private const string RouteValuesDictionaryName = "asp-all-route-data";
    private const string RouteValuesPrefix = "asp-route-";
    [HtmlAttributeName(RouteValuesDictionaryName, DictionaryAttributePrefix = RouteValuesPrefix)]
    public IDictionary<string, string> RouteValues { get; set; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

    private const string ActionAttributeName = "asp-action";
    [HtmlAttributeName(ActionAttributeName)]
    public string? AspAction { get; set; }

    private const string ControllerAttributeName = "asp-controller";
    [HtmlAttributeName(ControllerAttributeName)]
    public string? AspController { get; set; }

    private const string AreaAttributeName = "asp-area";
    [HtmlAttributeName(AreaAttributeName)]
    public string? AspArea { get; set; }

    private const string OptionsAttributeName = "options";
    [HtmlAttributeName(OptionsAttributeName)]
    public PagedListRenderOptions? Options { get; set; }

    private const string ParamPageNumberAttributeName = "param-page-number";
    [HtmlAttributeName(ParamPageNumberAttributeName)]
    public string ParamPageNumber { get; set; } = "page";

    [HtmlAttributeNotBound]
    [ViewContext]
    public ViewContext ViewContext { get; set; } = default!;

    private readonly IUrlHelperFactory _urlHelperFactory;

    public PagerTagHelper(IUrlHelperFactory urlHelperFactory)
    {
        _urlHelperFactory = urlHelperFactory;
    }

    private string? GeneratePageUrl(int pageNumber, IUrlHelper urlHelper)
    {
        var routeValues = new RouteValueDictionary();
        foreach (var kv in RouteValues)
            routeValues[kv.Key] = kv.Value;
        routeValues[ParamPageNumber] = pageNumber;
        if (AspArea != null)
            routeValues["area"] = AspArea;
        return urlHelper.Action(AspAction, AspController, routeValues);
    }

    private static TagBuilder WrapInListItem(string text)
    {
        var li = new TagBuilder("li");
        li.InnerHtml.AppendHtml(text);
        return li;
    }

    private static TagBuilder WrapInListItem(TagBuilder inner, params string[] classes)
    {
        var li = new TagBuilder("li");
        foreach (var c in classes)
            li.AddCssClass(c);
        li.InnerHtml.AppendHtml(inner);
        return li;
    }

    private TagBuilder First(IUrlHelper urlHelper)
    {
        const int targetPage = 1;
        var a = new TagBuilder("a");
        foreach (var c in Options!.AhrefElementClasses) a.AddCssClass(c);
        a.InnerHtml.AppendHtml(string.Format(Options.LinkToFirstPageFormat, targetPage));
        if (List!.IsFirstPage)
        {
            a.Attributes["tabindex"] = "-1";
            a.Attributes["aria-disabled"] = "true";
            return WrapInListItem(a, Options.DisabledElementClasses.ToArray());
        }
        a.Attributes["href"] = GeneratePageUrl(targetPage, urlHelper);
        return WrapInListItem(a);
    }

    private TagBuilder Previous(IUrlHelper urlHelper)
    {
        var targetPage = List!.PageNumber - 1;
        var a = new TagBuilder("a");
        foreach (var c in Options!.AhrefElementClasses) a.AddCssClass(c);
        a.InnerHtml.AppendHtml(string.Format(Options.LinkToPreviousPageFormat, targetPage));
        a.Attributes["rel"] = "prev";
        if (!List.HasPreviousPage)
        {
            a.Attributes["tabindex"] = "-1";
            a.Attributes["aria-disabled"] = "true";
            return WrapInListItem(a, Options.DisabledElementClasses.ToArray());
        }
        a.Attributes["href"] = GeneratePageUrl(targetPage, urlHelper);
        return WrapInListItem(a);
    }

    private TagBuilder Page(int pageNumber, IUrlHelper urlHelper)
    {
        var isCurrent = pageNumber == List!.PageNumber;
        var tag = new TagBuilder(isCurrent ? "span" : "a");
        foreach (var c in Options!.AhrefElementClasses) tag.AddCssClass(c);
        tag.InnerHtml.AppendHtml(string.Format(Options.LinkToIndividualPageFormat, pageNumber));
        if (isCurrent)
        {
            tag.Attributes["aria-current"] = "page";
            return WrapInListItem(tag, Options.ActiveElementClasses.ToArray());
        }
        tag.Attributes["href"] = GeneratePageUrl(pageNumber, urlHelper);
        return WrapInListItem(tag);
    }

    private TagBuilder Next(IUrlHelper urlHelper)
    {
        var targetPage = List!.PageNumber + 1;
        var a = new TagBuilder("a");
        foreach (var c in Options!.AhrefElementClasses) a.AddCssClass(c);
        a.InnerHtml.AppendHtml(string.Format(Options.LinkToNextPageFormat, targetPage));
        a.Attributes["rel"] = "next";
        if (!List.HasNextPage)
        {
            a.Attributes["tabindex"] = "-1";
            a.Attributes["aria-disabled"] = "true";
            return WrapInListItem(a, Options.DisabledElementClasses.ToArray());
        }
        a.Attributes["href"] = GeneratePageUrl(targetPage, urlHelper);
        return WrapInListItem(a);
    }

    private TagBuilder Last(IUrlHelper urlHelper)
    {
        var targetPage = List!.PageCount;
        var a = new TagBuilder("a");
        foreach (var c in Options!.AhrefElementClasses) a.AddCssClass(c);
        a.InnerHtml.AppendHtml(string.Format(Options.LinkToLastPageFormat, targetPage));
        if (List.IsLastPage)
        {
            a.Attributes["tabindex"] = "-1";
            a.Attributes["aria-disabled"] = "true";
            return WrapInListItem(a, Options.DisabledElementClasses.ToArray());
        }
        a.Attributes["href"] = GeneratePageUrl(targetPage, urlHelper);
        return WrapInListItem(a);
    }

    private TagBuilder PageCountAndLocationText()
    {
        var a = new TagBuilder("a");
        a.InnerHtml.AppendHtml(string.Format(Options!.PageCountAndCurrentLocationFormat, List!.PageNumber, List.PageCount));
        return WrapInListItem(a, Options.DisabledElementClasses.ToArray());
    }

    private TagBuilder ItemSliceAndTotalText()
    {
        var a = new TagBuilder("a");
        a.InnerHtml.AppendHtml(string.Format(Options!.ItemSliceAndTotalFormat, List!.FirstItemOnPage, List.LastItemOnPage, List.TotalItemCount));
        return WrapInListItem(a, Options.DisabledElementClasses.ToArray());
    }

    private TagBuilder Ellipses()
    {
        var a = new TagBuilder("a");
        foreach (var c in Options!.AhrefElementClasses) a.AddCssClass(c);
        a.InnerHtml.AppendHtml(Options.EllipsesFormat);
        return WrapInListItem(a, Options.DisabledElementClasses.ToArray());
    }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (List == null) return;
        Options ??= PagedListRenderOptions.Bootstrap4PageNumbersPlusPrevAndNext;

        var urlHelper = _urlHelperFactory.GetUrlHelper(ViewContext);
        var listItemLinks = new List<TagBuilder>();

        var firstPage = 1;
        var lastPage = List.PageCount;
        var pageCount = lastPage;

        if (Options.MaximumPageNumbersToDisplay.HasValue && List.PageCount > Options.MaximumPageNumbersToDisplay)
        {
            var max = Options.MaximumPageNumbersToDisplay.Value;
            firstPage = List.PageNumber - max / 2;
            if (firstPage < 1) firstPage = 1;
            pageCount = max;
            lastPage = firstPage + pageCount - 1;
            if (lastPage > List.PageCount)
            {
                lastPage = List.PageCount;
                firstPage = List.PageCount - max + 1;
            }
        }

        if (Options.DisplayLinkToFirstPage == PagedListDisplayMode.Always ||
            (Options.DisplayLinkToFirstPage == PagedListDisplayMode.IfNeeded && firstPage > 1))
            listItemLinks.Add(First(urlHelper));

        if (Options.DisplayLinkToPreviousPage == PagedListDisplayMode.Always ||
            (Options.DisplayLinkToPreviousPage == PagedListDisplayMode.IfNeeded && !List.IsFirstPage))
            listItemLinks.Add(Previous(urlHelper));

        if (Options.DisplayPageCountAndCurrentLocation)
            listItemLinks.Add(PageCountAndLocationText());

        if (Options.DisplayItemSliceAndTotal)
            listItemLinks.Add(ItemSliceAndTotalText());

        if (Options.DisplayLinkToIndividualPages)
        {
            if (Options.DisplayEllipsesWhenNotShowingAllPageNumbers && firstPage > 1)
                listItemLinks.Add(Ellipses());

            for (int i = firstPage; i <= lastPage; i++)
            {
                if (i > firstPage && !string.IsNullOrWhiteSpace(Options.DelimiterBetweenPageNumbers))
                    listItemLinks.Add(WrapInListItem(Options.DelimiterBetweenPageNumbers));
                listItemLinks.Add(Page(i, urlHelper));
            }

            if (Options.DisplayEllipsesWhenNotShowingAllPageNumbers && firstPage + pageCount - 1 < List.PageCount)
                listItemLinks.Add(Ellipses());
        }

        if (Options.DisplayLinkToNextPage == PagedListDisplayMode.Always ||
            (Options.DisplayLinkToNextPage == PagedListDisplayMode.IfNeeded && !List.IsLastPage))
            listItemLinks.Add(Next(urlHelper));

        if (Options.DisplayLinkToLastPage == PagedListDisplayMode.Always ||
            (Options.DisplayLinkToLastPage == PagedListDisplayMode.IfNeeded && lastPage < List.PageCount))
            listItemLinks.Add(Last(urlHelper));

        if (listItemLinks.Count > 0)
        {
            if (!string.IsNullOrWhiteSpace(Options.ClassToApplyToFirstListItemInPager))
                listItemLinks.First().AddCssClass(Options.ClassToApplyToFirstListItemInPager);
            if (!string.IsNullOrWhiteSpace(Options.ClassToApplyToLastListItemInPager))
                listItemLinks.Last().AddCssClass(Options.ClassToApplyToLastListItemInPager);
            foreach (var li in listItemLinks)
                foreach (var c in Options.LiElementClasses ?? [])
                    li.AddCssClass(c);
        }

        output.TagName = string.IsNullOrWhiteSpace(Options.ContainerHtmlTag) ? "div" : Options.ContainerHtmlTag;
        output.TagMode = TagMode.StartTagAndEndTag;

        var ul = new TagBuilder("ul");
        foreach (var li in listItemLinks)
            ul.InnerHtml.AppendHtml(li);
        if (Options.UlElementClasses != null)
            foreach (var c in Options.UlElementClasses)
                ul.AddCssClass(c);

        output.Content.AppendHtml(ul);
    }
}
