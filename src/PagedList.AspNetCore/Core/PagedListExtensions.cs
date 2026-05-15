namespace PagedList;

/// <summary>Extension methods for creating paged lists from collections.</summary>
public static class PagedListExtensions
{
    /// <summary>Creates a paged list from an <see cref="IEnumerable{T}"/>.</summary>
    public static IPagedList<T> ToPagedList<T>(this IEnumerable<T> superset, int pageNumber, int pageSize)
        => new PagedList<T>(superset, pageNumber, pageSize);

    /// <summary>Creates a paged list from an <see cref="IQueryable{T}"/>.</summary>
    public static IPagedList<T> ToPagedList<T>(this IQueryable<T> superset, int pageNumber, int pageSize)
        => new PagedList<T>(superset, pageNumber, pageSize);

    /// <summary>Splits a collection into n equal-ish pages.</summary>
    public static IEnumerable<IEnumerable<T>> Split<T>(this IEnumerable<T> superset, int numberOfPages)
        => superset
            .Select((item, index) => new { index, item })
            .GroupBy(x => x.index % numberOfPages)
            .Select(x => x.Select(y => y.item));

    /// <summary>Partitions a collection into pages of at most <paramref name="pageSize"/> items.</summary>
    public static IEnumerable<IEnumerable<T>> Partition<T>(this IEnumerable<T> superset, int pageSize)
    {
        var list = superset.ToList();
        if (list.Count <= pageSize)
        {
            yield return list;
            yield break;
        }
        var numberOfPages = (int)Math.Ceiling(list.Count / (double)pageSize);
        for (var i = 0; i < numberOfPages; i++)
            yield return list.Skip(pageSize * i).Take(pageSize);
    }
}
