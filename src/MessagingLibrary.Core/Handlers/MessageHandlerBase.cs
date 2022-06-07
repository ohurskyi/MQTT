using MessagingLibrary.Core.Extensions;
using MessagingLibrary.Core.Messages;
using MessagingLibrary.Core.Results;

namespace MessagingLibrary.Core.Handlers;

public abstract class MessageHandlerBase<T> : IMessageHandler
    where T: IMessagePayload
{
    public Task<IExecutionResult> Handle(IMessage message)
    {
        return HandleAsync(message.Payload.MessagePayloadFromJson<T>());
    }

    protected abstract Task<IExecutionResult> HandleAsync(T payload);
}