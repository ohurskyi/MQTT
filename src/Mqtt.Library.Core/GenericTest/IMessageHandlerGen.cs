namespace Mqtt.Library.Core.GenericTest;

public interface IMessageHandlerGen
{
    Task Handle(IMessage message);
}