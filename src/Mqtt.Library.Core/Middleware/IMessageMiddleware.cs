using Mqtt.Library.Core.Messages;

namespace Mqtt.Library.Core.Middleware;

public delegate Task MessageHandlerDelegate();

public interface IMessageMiddleware
{
    Task Handle(IMessage message, MessageHandlerDelegate next);
}