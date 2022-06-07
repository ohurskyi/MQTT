using MessagingLibrary.Core.Handlers;
using MessagingLibrary.Core.Messages;
using MessagingLibrary.Core.Results;

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
            return await Task.FromResult(ExecutionResult.Fail($"Not existing correlation id {message.CorrelationId}"));
        }

        taskCompletionSource.TrySetResult(message.Payload);
            
        return await Task.FromResult(ExecutionResult.Ok());
    }
}