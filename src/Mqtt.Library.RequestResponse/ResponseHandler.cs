using Mqtt.Library.Core;
using Mqtt.Library.Core.Messages;
using Mqtt.Library.Core.Results;

namespace Mqtt.Library.RequestResponse;

public class ResponseHandler : IMessageHandler
{
    private readonly PendingResponsesTracker _pendingResponsesTracker;

    public ResponseHandler(PendingResponsesTracker pendingResponsesTracker)
    {
        _pendingResponsesTracker = pendingResponsesTracker;
    }

    public async Task<IExecutionResult> Handle(IMessage message)
    {
        if (!_pendingResponsesTracker.TaskCompletionSources.TryRemove(message.CorrelationId, out var taskCompletionSource))
        {
            return await Task.FromResult(ExecutionResult.Ok());
        }

        if (!(message is IMessageResponse commandResponse))
        {
            return await Task.FromResult(ExecutionResult.Ok());
        }
            
        taskCompletionSource.TrySetResult(commandResponse);
            
        return await Task.FromResult(ExecutionResult.Ok());
    }
}