using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.DI;
using SPTarkov.Server.Core.Utils;
using SPTLeaderboardV2Server.Callbacks;
using SPTLeaderboardV2Server.Models.Requests;

namespace SPTLeaderboardV2Server.Routers;

[Injectable(TypePriority = OnLoadOrder.PostDBModLoader + 1)]
public class ItemRouter : StaticRouter
{
    private static ItemCallbacks _callbacks = null!;

    public ItemRouter(JsonUtil jsonUtil, ItemCallbacks callbacks) : base(jsonUtil, GetRoutes())
    {
        _callbacks = callbacks;
    }

    private static List<RouteAction> GetRoutes()
        => [
            new RouteAction<ItemPricesRequestData>("/SPTLB/GetItemPrices", async (url, data, sessionId, output) => await _callbacks.HandleItemPrices(data))
        ];
}
