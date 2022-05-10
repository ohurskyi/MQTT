using Mqtt.Library.Core.Messages;

namespace Mqtt.Library.Core.Strategy;

public interface IMessageHandlingStrategy
{
    Task Handle(IMessage message);
}