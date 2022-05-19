using Mqtt.Library.Core.Extensions;
using Mqtt.Library.Core.Messages;

namespace Mqtt.Library.Core;

public abstract class MessageHandlerBase<T> : IMessageHandler
    where T: IMessagePayload
{
    public Task Handle(IMessage message)
    {
        return HandleAsync(message.Payload.MessagePayloadFromJson<T>());
    }

    protected abstract Task HandleAsync(T payload);
}