using Mqtt.Library.Core.Extensions;
using Mqtt.Library.Core.Messages;
using Mqtt.Library.Core.Results;

namespace Mqtt.Library.Core;

public abstract class MessageHandlerBase<T> : IMessageHandler
    where T: IMessagePayload
{
    public Task<IExecutionResult> Handle(IMessage message)
    {
        return HandleAsync(message.Payload.MessagePayloadFromJson<T>());
    }

    protected abstract Task<IExecutionResult> HandleAsync(T payload);
}