using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.Models.Common;
using SPTarkov.Server.Core.Models.Utils;
using SPTarkov.Server.Core.Utils;
using SPTLeaderboardV2Server.Models;
using SPTLeaderboardV2Server.Utils;

namespace SPTLeaderboardV2Server.Callbacks;

[Injectable]
public class InboxCallbacks(
    SessionInboxChecks inboxChecks,
    HttpResponseUtil httpResponseUtil,
    InboxUtils inboxUtils,
    ISptLogger<InboxCallbacks> logger)
{
    public ValueTask<object> HandleInboxNotChecked(MongoId sessionId, string? output)
    {
        logger.Debug($"[SPT Leaderboard v2] {sessionId} not checked");
        if (!inboxChecks.TrySetSessionInboxState(sessionId, false))
        {
            logger.Debug($"Added {sessionId} to inbox checks");
            inboxChecks.AddSessionInboxState(sessionId, false);
        }

        return new ValueTask<object>(httpResponseUtil.NoBody(output));
    }

    public ValueTask<object> HandleInboxChecked(MongoId sessionId, string? output)
    {
        logger.Debug($"{sessionId} is checked");
        Task.Run(async () =>
        {
            try
            {
                await inboxUtils.CheckInbox(sessionId);
            }
            catch (Exception ex)
            {
                logger.Error($"[SPT Leaderboard v2] Inbox check failed for {sessionId}: {ex.Message}");
            }
        });
        if (!inboxChecks.TrySetSessionInboxState(sessionId, true))
        {
            logger.Debug($"Added {sessionId} to inbox checks");
            inboxChecks.AddSessionInboxState(sessionId, true);
        }
        return new ValueTask<object>(httpResponseUtil.NoBody(output));
    }
}
