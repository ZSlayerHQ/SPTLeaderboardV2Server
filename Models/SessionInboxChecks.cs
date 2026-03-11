using System.Collections.Concurrent;
using SPTarkov.DI.Annotations;
using SPTarkov.Server.Core.Models.Common;

namespace SPTLeaderboardV2Server.Models;

[Injectable(InjectionType.Singleton)]
public class SessionInboxChecks
{
    private readonly ConcurrentDictionary<MongoId, bool> _inboxChecks = new();

    public void AddSessionInboxState(MongoId sessionId, bool inboxState)
    {
        _inboxChecks.TryAdd(sessionId, inboxState);
    }

    public bool TrySetSessionInboxState(MongoId sessionId, bool newChecked)
    {
        if (!_inboxChecks.ContainsKey(sessionId))
        {
            return false;
        }

        _inboxChecks[sessionId] = newChecked;
        return true;
    }

    public bool TryGetSessionInboxState(MongoId sessionId, out bool inboxState)
    {
        return _inboxChecks.TryGetValue(sessionId, out inboxState);
    }
}
