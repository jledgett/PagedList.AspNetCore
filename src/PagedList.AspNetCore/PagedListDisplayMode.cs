namespace PagedList.AspNetCore;

/// <summary>
/// Controls the visibility of portions of the paging control.
/// </summary>
public enum PagedListDisplayMode
{
    /// <summary>Always render.</summary>
    Always,

    /// <summary>Never render.</summary>
    Never,

    /// <summary>Only render when there is contextually relevant data to show.</summary>
    IfNeeded
}
