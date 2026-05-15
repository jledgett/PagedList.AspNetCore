# PagedList.AspNetCore

A Bootstrap-compatible pager tag helper for ASP.NET Core, targeting **.NET 10+**.

## Why does this exist?

The original paging ecosystem for ASP.NET Core was split across two NuGet packages:

- **[PagedList.Core](https://www.nuget.org/packages/PagedList.Core)** — core paging types (`IPagedList`, `StaticPagedList<T>`, `PagedList<T>`, `.ToPagedList()`)
- **[PagedList.Core.Mvc](https://www.nuget.org/packages/PagedList.Core.Mvc)** — the Razor tag helper (`<pager>`) and render options

Both packages were abandoned after .NET 5 and are not optimized with modern .NET versions.   This package replaces both with a single, maintained package that:

- Targets .NET 10+ with the latest security and performance improvements
- Ships all paging types and the MVC tag helper in one install
- Requires no third-party dependencies beyond the ASP.NET Core framework
- Allows you to [upgrade pre-existing projects](#upgrading-from-pagedlistcore--pagedlistcoremvc) in less than 10 minutes

## Installation

```shell
dotnet add package PagedList.AspNetCore
```

That's it — no second package needed.

## Usage

### 1. Register the tag helper

In `Views/_ViewImports.cshtml`:

```cshtml
@addTagHelper *, PagedList.AspNetCore
```

### 2. Create a paged list in your controller

```csharp
using PagedList;

public IActionResult Index(int? page)
{
    var pageNumber = page ?? 1;
    var pageSize = 10;

    var items = _db.Products.OrderBy(p => p.Name);
    var pagedItems = items.ToPagedList(pageNumber, pageSize);

    return View(pagedItems);
}
```

If you are paging data yourself (e.g., from a search API or stored procedure):

```csharp
using PagedList;

var pageOfItems = new StaticPagedList<Product>(itemsForThisPage, pageNumber, pageSize, totalCount);
```

### 3. Add the pager to your view

```cshtml
@using PagedList.AspNetCore
@model IPagedList<Product>

@foreach (var item in Model) { ... }

<pager list="@Model"
       asp-action="Index"
       asp-controller="Products" />
```

### Default render options

The pager defaults to Bootstrap 4/5 page-numbers-plus-prev-next style. Pass an `options` attribute to change it:

```cshtml
<pager list="@Model"
       options="@PagedListRenderOptions.Bootstrap4Minimal"
       asp-action="Index"
       asp-controller="Products" />
```

Available presets:

| Option | Description |
|--------|-------------|
| `Bootstrap4Minimal` | Previous / Next only |
| `Bootstrap4PageNumbersOnly` | Page numbers only |
| `Bootstrap4PageNumbersPlusPrevAndNext` | Page numbers + Previous / Next *(default)* |
| `Bootstrap4PageNumbersPlusFirstAndLast` | Page numbers + First / Last |
| `Bootstrap4Full` | All navigation links |

These work with both Bootstrap 4 and Bootstrap 5 — the pagination CSS class structure is identical between versions.

### Custom options

```csharp
var options = PagedListRenderOptions.Bootstrap4Full;
options.MaximumPageNumbersToDisplay = 5;
```

### Passing route values

```cshtml
<pager list="@Model"
       asp-action="Index"
       asp-controller="Search"
       asp-route-query="@ViewBag.Query"
       asp-route-category="@ViewBag.Category" />
```

## Upgrading from PagedList.Core + PagedList.Core.Mvc

> **Important:** Upgrade your application to .NET 10+ and verify it builds and runs correctly **before** swapping this package in. Mixing a .NET 10 host with old .NET 5 packages will cause runtime failures that are unrelated to this library.

Once your app is running on .NET 10:

### 1. Remove the old packages

```shell
dotnet remove package PagedList.Core
dotnet remove package PagedList.Core.Mvc
```

### 2. Install this package

```shell
dotnet add package PagedList.AspNetCore
```

### 3. Update namespaces

Find and replace across your project:

| Old | New |
|-----|-----|
| `using PagedList.Core.Mvc;` | `using PagedList.AspNetCore;` |
| `using PagedList.Core;` | `using PagedList;` |

### 4. Update the tag helper registration

In `Views/_ViewImports.cshtml`:

```cshtml
@* Remove this: *@
@addTagHelper *, PagedList.Core.Mvc

@* Add this: *@
@addTagHelper *, PagedList.AspNetCore
```

### 5. Build and test

Everything else — tag helper attributes (`list`, `options`, `asp-action`, `asp-route-*`, `param-page-number`), `PagedListRenderOptions` properties, `IPagedList<T>`, `StaticPagedList<T>` — is unchanged.

## License

MIT — see original projects by [Troy Goode](https://github.com/TroyGoode/PagedList) and [Hieu Le](https://github.com/hieudole/PagedList.Core.Mvc).
