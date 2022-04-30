using Mqtt.Library.Core;
using Mqtt.Library.Core.Messages;
using Mqtt.Library.Test.Payloads;
using MQTTnet;
using Serilog;

namespace Mqtt.Library.Test.Handlers;

public class HandlerForDeviceNumber1 : IMessageHandler, IDisposable
{
    private readonly ILogger<HandlerForDeviceNumber1> _logger;

    public HandlerForDeviceNumber1(ILogger<HandlerForDeviceNumber1> logger)
    {
        _logger = logger;
    }

    public Task Handle(IMessage message)
    {
        _logger.LogInformation("Handler {handler} received message from topic = {value}", nameof(HandlerForDeviceNumber1), message.Topic);
        var deviceMessagePayload = message.FromJson<DeviceMessagePayload>();
        _logger.LogInformation("Device name = {value}", deviceMessagePayload.Name);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _logger.LogInformation($"{nameof(HandlerForDeviceNumber1)} disposed.");;
    }
}