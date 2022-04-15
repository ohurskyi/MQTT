using Mqtt.Library.Core;
using MQTTnet;

namespace Mqtt.Library.Test.Handlers;

public class HandlerForDeviceNumber2 : IMessageHandler, IDisposable
{
    private readonly ILogger<HandlerForDeviceNumber2> _logger;

    public HandlerForDeviceNumber2(ILogger<HandlerForDeviceNumber2> logger)
    {
        _logger = logger;
    }

    public Task Handle(MqttApplicationMessage mqttApplicationMessage)
    {
        _logger.LogInformation("Handler {handler} received message from topic = {value}", nameof(HandlerForDeviceNumber2), mqttApplicationMessage.Topic);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _logger.LogInformation($"{nameof(HandlerForDeviceNumber2)} disposed.");;
    }
}