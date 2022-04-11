using Mqtt.Library.Test.Core;
using MQTTnet;

namespace MqttClientTest.Messaging.Processing;

public class MessageHandlerWrapper
{
    public async Task Handle(MqttApplicationMessage mqttApplicationMessage, IMessageHandlerFactory messageHandlerFactory)
    {
        var handlers = messageHandlerFactory.GetHandlers(mqttApplicationMessage.Topic);

        var funcs = handlers.Select(h => new Func<MqttApplicationMessage, Task>(h.Handle));

        await HandleStrategy(funcs, mqttApplicationMessage);
    }

    protected virtual async Task HandleStrategy(IEnumerable<Func<MqttApplicationMessage, Task>> handlers, MqttApplicationMessage mqttApplicationMessage)
    {
        foreach (var handler in handlers)
        {
            await handler(mqttApplicationMessage);
        }
    }
}