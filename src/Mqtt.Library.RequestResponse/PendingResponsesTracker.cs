using System.Collections.Concurrent;

namespace Mqtt.Library.RequestResponse;

public class PendingResponsesTracker
{
    public readonly ConcurrentDictionary<Guid, TaskCompletionSource<IMessageResponse>> TaskCompletionSources = new();

    public Task<IMessageResponse> AddCompletionSource(Guid correlationId)
    {
        var tcs = new TaskCompletionSource<IMessageResponse>();
        TaskCompletionSources.TryAdd(correlationId, tcs);
        return tcs.Task;
    }
}