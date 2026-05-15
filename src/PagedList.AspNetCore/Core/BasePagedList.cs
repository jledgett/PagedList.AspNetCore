namespace PagedList;

/// <summary>
/// Base class for paged list implementations.
/// </summary>
/// <typeparam name="T">The type of object the collection should contain.</typeparam>
public abstract class BasePagedList<T> : PagedListMetaData, IPagedList<T>
{
    protected readonly List<T> Subset = [];

    protected internal BasePagedList() { }

    protected internal BasePagedList(int pageNumber, int pageSize, int totalItemCount)
    {
        ArgumentOutOfRangeException.ThrowIfLessThan(pageNumber, 1, nameof(pageNumber));
        ArgumentOutOfRangeException.ThrowIfLessThan(pageSize, 1, nameof(pageSize));

        TotalItemCount = totalItemCount;
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
    }

    public IEnumerator<T> GetEnumerator() => Subset.GetEnumerator();
    System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator() => GetEnumerator();

    public T this[int index] => Subset[index];
    public int Count => Subset.Count;
    public IPagedList GetMetaData() => new PagedListMetaData(this);
}
