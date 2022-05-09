using Mqtt.Library.Core.Messages;

namespace Mqtt.Library.Core;

public interface IMessageHandlingStrategy
{
    Task Handle(IMessage message);
}