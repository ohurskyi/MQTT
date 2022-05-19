using Mqtt.Library.Core.Extensions;
using Mqtt.Library.Core.Messages;

namespace Mqtt.Library.Core;

public abstract class MessageHandlerBase<T> : IMessageHandler
{
    public Task Handle(IMessage message)
    {
        return HandleAsync(message.FromJson<T>());
    }

    protected abstract Task HandleAsync(T payload);
}