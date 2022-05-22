using Microsoft.Extensions.Logging;
using Mqtt.Library.Core;
using Mqtt.Library.Core.Messages;

namespace MqttLibrary.Examples.Domain.Handlers;

public class HandlerForDeviceNumber1ForAnotherClient : IMessageHandler, IDisposable
{
    private readonly ILogger<HandlerForDeviceNumber1ForAnotherClient> _logger;

    public HandlerForDeviceNumber1ForAnotherClient(ILogger<HandlerForDeviceNumber1ForAnotherClient> logger)
    {
        _logger = logger;
    }

    public Task Handle(IMessage message)
    {
        _logger.LogInformation("Handler {handler} received message from topic = {value}", nameof(HandlerForDeviceNumber1ForAnotherClient), message.Topic);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _logger.LogInformation($"{nameof(HandlerForDeviceNumber1ForAnotherClient)} disposed.");;
    }
}