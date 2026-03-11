using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.Utils;
using SPTLeaderboardV2Server.Models.Requests;
using SPTLeaderboardV2Server.Utils;

namespace SPTLeaderboardV2Server.Callbacks;

[Injectable]
public class ItemCallbacks(HttpResponseUtil httpResponseUtil, ItemUtils itemUtils)
{
    public ValueTask<string> HandleItemPrices(ItemPricesRequestData requestData)
    {
        return new ValueTask<string>(httpResponseUtil.NoBody(itemUtils.GetTotalHandBookPrice(requestData.TemplateIds)));
    }
}
