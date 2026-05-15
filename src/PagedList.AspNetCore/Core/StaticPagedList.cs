namespace PagedList;

/// <summary>
/// A paged list whose subset has already been divided externally. Use when you have done the paging yourself (e.g., via a database query) and just need to wrap the page's items with metadata.
/// </summary>
/// <typeparam name="T">The type of object the collection should contain.</typeparam>
public class StaticPagedList<T> : BasePagedList<T>
{
    /// <summary>
    /// Initializes a new instance using metadata copied from an existing <see cref="IPagedList"/>.
    /// </summary>
    public StaticPagedList(IEnumerable<T> subset, IPagedList metaData)
        : this(subset, metaData.PageNumber, metaData.PageSize, metaData.TotalItemCount) { }

    /// <summary>
    /// Initializes a new instance with the already-divided subset and superset size information.
    /// </summary>
    public StaticPagedList(IEnumerable<T> subset, int pageNumber, int pageSize, int totalItemCount)
        : base(pageNumber, pageSize, totalItemCount)
    {
        Subset.AddRange(subset);
    }
}
