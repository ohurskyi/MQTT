using Mqtt.Library.Core;
using Mqtt.Library.Core.GenericTest;
using MQTTnet;

namespace Mqtt.Library.Test.Handlers;

public class HandlerForAllDeviceNumbers : IMessageHandlerGen, IDisposable
{
    private readonly ILogger<HandlerForAllDeviceNumbers> _logger;

    public HandlerForAllDeviceNumbers(ILogger<HandlerForAllDeviceNumbers> logger)
    {
        _logger = logger;
    }

    public Task Handle(IMessage message)
    {
        _logger.LogInformation("Handler {handler} received message from topic = {value}", nameof(HandlerForAllDeviceNumbers), message.Topic);
        return Task.Delay(TimeSpan.FromSeconds(1));
    }

    public void Dispose()
    {
        _logger.LogInformation($"{nameof(HandlerForAllDeviceNumbers)} disposed.");
    }
}