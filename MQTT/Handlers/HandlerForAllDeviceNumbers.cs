using Mqtt.Library.Core;
using MQTTnet;

namespace Mqtt.Library.Test.Handlers;

public class HandlerForAllDeviceNumbers : IMessageHandler, IDisposable
{
    private readonly ILogger<HandlerForAllDeviceNumbers> _logger;

    public HandlerForAllDeviceNumbers(ILogger<HandlerForAllDeviceNumbers> logger)
    {
        _logger = logger;
    }

    public Task Handle(MqttApplicationMessage mqttApplicationMessage)
    {
        _logger.LogInformation("Handler {handler} received message from topic = {value}", nameof(HandlerForAllDeviceNumbers), mqttApplicationMessage.Topic);
        return Task.Delay(TimeSpan.FromSeconds(1));
    }

    public void Dispose()
    {
        _logger.LogInformation($"{nameof(HandlerForAllDeviceNumbers)} disposed.");
    }
}