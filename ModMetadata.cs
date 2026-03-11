using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Models.Spt.Mod;
using SPTarkov.Server.Core.Models.Utils;

namespace SPTLeaderboardV2Server;

public record ModMetadata : AbstractModMetadata
{
    public const string StaticVersion = "1.0.0";

    public override string ModGuid { get; init; } = "com.sptleaderboard.v2.server";
    public override string Name { get; init; } = "SPT Leaderboard v2 Server";
    public override string Author { get; init; } = "ZSlayerHQ";
    public override List<string>? Contributors { get; init; } = ["Harmony", "Katrin0522", "yuyui.moe", "RuKira"];
    public override SemanticVersioning.Version Version { get; init; } = new(StaticVersion);
    public override SemanticVersioning.Range SptVersion { get; init; } = new("~4.0.x");

    public override List<string>? Incompatibilities { get; init; }
    public override Dictionary<string, SemanticVersioning.Range>? ModDependencies { get; init; }
    public override string? Url { get; init; } = "https://github.com/ZSlayerHQ/SPTLeaderboardV2Server";
    public override bool? IsBundleMod { get; init; } = false;
    public override string? License { get; init; } = "MPL 2.0";
}

[Injectable(TypePriority = OnLoadOrder.PostSptModLoader + 1)]
public class Startup(ISptLogger<Startup> logger) : IOnLoad
{
    public Task OnLoad()
    {
        logger.Success($"[SPT Leaderboard v2] Server mod v{ModMetadata.StaticVersion} loaded.");
        return Task.CompletedTask;
    }
}
