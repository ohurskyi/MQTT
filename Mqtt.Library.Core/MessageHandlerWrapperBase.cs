using Microsoft.Extensions.DependencyInjection;
using MQTTnet;

namespace Mqtt.Library.Core;

public abstract class MessageHandlerWrapperBase
{
    public abstract Task Handle(MqttApplicationMessage mqttApplicationMessage, IMessageHandlerFactory messageHandlerFactory, IServiceScope serviceScope);
}