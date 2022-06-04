using System.Collections.Concurrent;
using Mqtt.Library.Core.Messages;

namespace Mqtt.Library.RequestResponse;

public class PendingResponsesTracker
{
    public readonly ConcurrentDictionary<Guid, TaskCompletionSource<string>> TaskCompletionSources = new();

    public Task<string> AddCompletionSource(Guid correlationId)
    {
        var tcs = new TaskCompletionSource<string>();
        TaskCompletionSources.TryAdd(correlationId, tcs);
        return tcs.Task;
    }
}