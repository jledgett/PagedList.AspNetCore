namespace PagedList;

/// <summary>
/// Divides a superset into pages and exposes one page by index.
/// </summary>
/// <typeparam name="T">The type of object the collection should contain.</typeparam>
public class PagedList<T> : BasePagedList<T>
{
    /// <summary>
    /// Initializes a new instance from an <see cref="IQueryable{T}"/> superset.
    /// </summary>
    public PagedList(IQueryable<T> superset, int pageNumber, int pageSize)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(pageNumber, 1, nameof(pageNumber));
        ArgumentOutOfRangeException.ThrowIfLessThan(pageSize, 1, nameof(pageSize));

        TotalItemCount = superset?.Count() ?? 0;
        PageSize = pageSize;
        PageNumber = pageNumber;
        PageCount = TotalItemCount > 0 ? (int)Math.Ceiling(TotalItemCount / (double)PageSize) : 0;
        HasPreviousPage = PageNumber > 1;
        HasNextPage = PageNumber < PageCount;
        IsFirstPage = PageNumber == 1;
        IsLastPage = PageNumber >= PageCount;
        FirstItemOnPage = (PageNumber - 1) * PageSize + 1;
        var lastItemOnPage = FirstItemOnPage + PageSize - 1;
        LastItemOnPage = lastItemOnPage > TotalItemCount ? TotalItemCount : lastItemOnPage;

        if (superset != null && TotalItemCount > 0)
            Subset.AddRange(superset.Skip((pageNumber - 1) * pageSize).Take(pageSize));
    }

    /// <summary>
    /// Initializes a new instance from an <see cref="IEnumerable{T}"/> superset.
    /// </summary>
    public PagedList(IEnumerable<T> superset, int pageNumber, int pageSize)
        : this(superset.AsQueryable(), pageNumber, pageSize) { }
}
