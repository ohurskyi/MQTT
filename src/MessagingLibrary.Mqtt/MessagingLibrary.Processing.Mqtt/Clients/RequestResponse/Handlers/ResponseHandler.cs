using MessagingLibrary.Core.Handlers;
using MessagingLibrary.Core.Messages;
using MessagingLibrary.Core.Results;
using MessagingLibrary.Processing.Mqtt.Clients.RequestResponse.Completion;

namespace MessagingLibrary.Processing.Mqtt.Clients.RequestResponse.Handlers;

public class ResponseHandler : IMessageHandler
{
    private readonly PendingResponseTracker _pendingResponseTracker;

    public ResponseHandler(PendingResponseTracker pendingResponseTracker)
    {
        _pendingResponseTracker = pendingResponseTracker;
    }

    public async Task<IExecutionResult> Handle(IMessage message)
    {
        var taskCompletionSource = _pendingResponseTracker.GetCompletion(message.CorrelationId);

        if (taskCompletionSource == null)
        {
            return await Task.FromResult(FailedResult.Create($"Cannot complete the response. Non existing correlation id {message.CorrelationId}"));
        }

        taskCompletionSource.SetResult(message.Payload);
            
        return await Task.FromResult(new SuccessfulResult());
    }
}