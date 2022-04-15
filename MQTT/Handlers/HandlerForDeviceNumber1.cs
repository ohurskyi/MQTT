using Mqtt.Library.Core;
using MQTTnet;

namespace Mqtt.Library.Test.Handlers;

public class HandlerForDeviceNumber1 : IMessageHandler, IDisposable
{
    private readonly ILogger<HandlerForDeviceNumber1> _logger;

    public HandlerForDeviceNumber1(ILogger<HandlerForDeviceNumber1> logger)
    {
        _logger = logger;
    }

    public Task Handle(MqttApplicationMessage mqttApplicationMessage)
    {
        _logger.LogInformation("Handler {handler} received message from topic = {value}", nameof(HandlerForDeviceNumber1), mqttApplicationMessage.Topic);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _logger.LogInformation($"{nameof(HandlerForDeviceNumber1)} disposed.");;
    }
}