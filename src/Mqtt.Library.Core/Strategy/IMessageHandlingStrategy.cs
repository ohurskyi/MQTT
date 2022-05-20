using Mqtt.Library.Core.Messages;

namespace Mqtt.Library.Core.Strategy;

public interface IMessageHandlingStrategy<T> where T : class
{
    Task Handle(IMessage message);
}