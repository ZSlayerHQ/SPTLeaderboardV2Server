using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.Models.Common;
using SPTarkov.Server.Core.Models.Enums;
using SPTarkov.Server.Core.Models.Utils;
using SPTarkov.Server.Core.Services;
using SPTarkov.Server.Core.Utils;
using SPTLeaderboardV2Server.Models.Responses;

namespace SPTLeaderboardV2Server.Utils;

[Injectable(InjectionType.Singleton)]
public class InboxUtils(
    ItemUtils itemUtils,
    JsonUtil jsonUtil,
    MailSendService mailSendService,
    ISptLogger<InboxUtils> logger)
{
    private static readonly HttpClient HttpClient = new();

    public async Task<bool> CheckInbox(MongoId sessionId)
    {
        try
        {
            var uri = new UriBuilder("https://sptlb.katrinfoxvr.com")
            {
                Path = "/api/main/inbox/checkInbox.php",
                Query = $"sessionId={sessionId}",
            };

            var request = await HttpClient.GetAsync(uri.Uri.AbsoluteUri);
            if (!request.IsSuccessStatusCode)
            {
                logger.Error($"[SPT Leaderboard v2] Inbox check failed with status {request.StatusCode}");
                return false;
            }

            var response = await request.Content.ReadAsStringAsync();
            var data = jsonUtil.Deserialize<CheckInboxResponseData>(response);
            if (data == null)
            {
                logger.Error("[SPT Leaderboard v2] Inbox response JSON deserialized to NULL");
                return false;
            }

            if (data.Status != "success")
            {
                logger.Debug($"[SPT Leaderboard v2] No new messages for SessionID {sessionId}");
                return true;
            }

            var generatedItems = itemUtils.GetItemInstancesAsFiR(data.Items ?? []);
            mailSendService.SendDirectNpcMessageToPlayer(
                sessionId,
                "54cb50c76803fa8b248b4571", // Prapor
                MessageType.MessageWithItems,
                data.Message ?? "[MESSAGE WITH NO CONTENTS]",
                generatedItems.ToList()
            );

            return true;
        }
        catch (Exception e)
        {
            logger.Error($"[SPT Leaderboard v2] Error checking player inbox: {e.Message} (SessionID: {sessionId})\n{e.StackTrace}");
            return false;
        }
    }
}
