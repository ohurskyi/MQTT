using Mqtt.Library.Core.Messages;

namespace Mqtt.Library.Core;

public interface IMessageHandler
{
    Task Handle(IMessage message);
}