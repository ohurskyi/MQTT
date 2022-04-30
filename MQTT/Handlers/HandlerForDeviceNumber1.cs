using Mqtt.Library.Core;
using Mqtt.Library.Core.GenericTest;
using MQTTnet;

namespace Mqtt.Library.Test.Handlers;

public class HandlerForDeviceNumber1 : IMessageHandlerGen, IDisposable
{
    private readonly ILogger<HandlerForDeviceNumber1> _logger;

    public HandlerForDeviceNumber1(ILogger<HandlerForDeviceNumber1> logger)
    {
        _logger = logger;
    }

    public Task Handle(IMessage message)
    {
        _logger.LogInformation("Handler {handler} received message from topic = {value}", nameof(HandlerForDeviceNumber1), message.Topic);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _logger.LogInformation($"{nameof(HandlerForDeviceNumber1)} disposed.");;
    }
}