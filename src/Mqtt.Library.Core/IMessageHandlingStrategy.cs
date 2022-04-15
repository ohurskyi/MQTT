using Microsoft.Extensions.DependencyInjection;
using MQTTnet;

namespace Mqtt.Library.Core;

public interface IMessageHandlingStrategy
{
    Task Handle(MqttApplicationMessage mqttApplicationMessage, IMessageHandlerFactory messageHandlerFactory, IServiceScope serviceScope);
}