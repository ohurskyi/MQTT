using Mqtt.Library.Core.Messages;
using Mqtt.Library.Core.Results;

namespace Mqtt.Library.Core.Middleware;

public delegate Task<HandlerResult> MessageHandlerDelegate();

public interface IMessageMiddleware
{
    Task<HandlerResult> Handle(IMessage message, MessageHandlerDelegate next);
}