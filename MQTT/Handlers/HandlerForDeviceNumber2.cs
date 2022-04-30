using Mqtt.Library.Core;
using Mqtt.Library.Core.Messages;
using MQTTnet;

namespace Mqtt.Library.Test.Handlers;

public class HandlerForDeviceNumber2 : IMessageHandler, IDisposable
{
    private readonly ILogger<HandlerForDeviceNumber2> _logger;

    public HandlerForDeviceNumber2(ILogger<HandlerForDeviceNumber2> logger)
    {
        _logger = logger;
    }

    public Task Handle(IMessage message)
    {
        _logger.LogInformation("Handler {handler} received message from topic = {value}", nameof(HandlerForDeviceNumber2), message.Topic);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _logger.LogInformation($"{nameof(HandlerForDeviceNumber2)} disposed.");;
    }
}