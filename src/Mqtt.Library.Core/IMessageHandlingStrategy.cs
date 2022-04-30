using Microsoft.Extensions.DependencyInjection;
using Mqtt.Library.Core.Factory;
using Mqtt.Library.Core.Messages;

namespace Mqtt.Library.Core;

public interface IMessageHandlingStrategy
{
    Task Handle(IMessage message, IMessageHandlerFactory messageHandlerFactory, IServiceScope serviceScope);
}