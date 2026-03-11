# SPT Leaderboard v2 — Server Mod

Server-side mod for [SPT (Single Player Tarkov)](https://www.sp-tarkov.com/) that powers the SPT Leaderboard v2 system. Handles profile data collection, inbox notifications, ragfair utilities, and API routes for the leaderboard backend.

## What Changed (v2 Modernization)

Forked from [SPT-Leaderboard/Server](https://github.com/SPT-Leaderboard/Server-CSharp) and modernized:

- **Updated to SPT 4.0.5** — NuGet packages `SPTarkov.Server.Core`, `SPTarkov.Common`, `SPTarkov.DI` all at 4.0.5
- **SDK-style .csproj** — modern project format, .NET 9.0, C# 12
- **Namespace renamed** — `SPTLeaderboard` → `SPTLeaderboardV2Server`
- **New GUID** — `com.sptleaderboard.v2.server`
- **Bug fixes:**
  - `SessionInboxChecks`: `Dictionary` → `ConcurrentDictionary` for thread-safe concurrent route access
  - `InboxCallbacks`: Added try-catch inside fire-and-forget `Task.Run()` to prevent silent failures
  - `RagfairUtils`: `Requirements?.First()` → `FirstOrDefault()` to prevent exceptions on empty collections
  - `ItemCallbacks`: Removed unused `using Microsoft.AspNetCore.Components`
- **Consistent logging** — all files use `ISptLogger<T>` (was mixed with `ILogger<T>`)
- **File-scoped namespaces** — modern C# style throughout

## Installation

1. Build: `dotnet build -c Release`
2. Copy output to `SPT/user/mods/SPTLeaderboardV2Server/`

## Requirements

- SPT 4.0.5+
- .NET 9.0 SDK (build only)

## Credits

Original authors: **Harmony**, **Katrin0522**, **yuyui.moe**, **RuKira**

## License

MIT — see [LICENSE](LICENSE)
