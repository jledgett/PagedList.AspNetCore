# Publishing PagedList.AspNetCore to NuGet.org

## Prerequisites

- [.NET 10 SDK](https://dotnet.microsoft.com/download) installed
- A [NuGet.org](https://www.nuget.org) account
- A NuGet API key with push permissions (see below)

---

## One-time setup

### 1. Create a NuGet.org account

Go to https://www.nuget.org and sign in or register.

### 2. Reserve the package ID

Before your first publish, claim the package ID so no one else can publish under it:

1. Go to https://www.nuget.org/packages/manage/upload
2. Upload any `.nupkg` — NuGet will register the ID on first push.

### 3. Generate an API key

1. Go to https://www.nuget.org/account/apikeys
2. Click **Create**
3. Set a name (e.g., `PagedList.AspNetCore publish`)
4. Under **Select Scopes**, choose **Push new packages and package versions**
5. Under **Select Packages**, choose **Specific patterns** and enter `PagedList.AspNetCore`
6. Set an expiry (365 days is reasonable)
7. Click **Create** and **copy the key** — it is only shown once

Store the key somewhere safe (a password manager, or as an environment variable `NUGET_API_KEY`).

---

## Build and pack

From the repo root:

```shell
cd src/PagedList.AspNetCore
dotnet pack --configuration Release --output ../../nupkg
```

This produces `nupkg/PagedList.AspNetCore.<version>.nupkg`.

Before publishing, verify the package contents:

```shell
# List what's inside the .nupkg (it's a zip)
Expand-Archive -Path nupkg/PagedList.AspNetCore.10.0.10.nupkg -DestinationPath nupkg/inspect -Force
Get-ChildItem nupkg/inspect -Recurse | Select-Object FullName
```

Check that:
- The `lib/net10.0/` folder contains `PagedList.AspNetCore.dll` and `PagedList.AspNetCore.xml`
- The `.nuspec` shows the correct version, description, and authors
- There are **no** `<dependencies>` entries (this is a self-contained package)

---

## Publish

From the repo root
```shell
dotnet nuget push nupkg/PagedList.AspNetCore.10.0.10.nupkg --api-key YOUR_API_KEY --source https://api.nuget.org/v3/index.json
```

Or with the environment variable:

```shell
dotnet nuget push nupkg/PagedList.AspNetCore.10.0.10.nupkg --api-key $env:NUGET_API_KEY --source https://api.nuget.org/v3/index.json
```

NuGet.org will validate and index the package within a few minutes. The package page will be live at:
https://www.nuget.org/packages/PagedList.AspNetCore

---

## Releasing a new version

1. Update `<Version>` in `src/PagedList.AspNetCore/PagedList.AspNetCore.csproj`
   - Follow the `MAJOR.MINOR.PATCH` convention where `MAJOR.MINOR` tracks the targeted .NET version (e.g., `10.0.*` for .NET 10)
2. Run the build + pack steps above
3. Push the new `.nupkg`

NuGet.org keeps all previous versions listed. Users stay on their pinned version until they explicitly upgrade.
