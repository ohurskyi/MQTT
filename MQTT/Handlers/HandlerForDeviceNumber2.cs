using Mqtt.Library.Core;
using Mqtt.Library.Core.Extensions;
using Mqtt.Library.Core.Messages;
using Mqtt.Library.Test.Payloads;
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
        var deviceMessagePayload = message.FromJson<DeviceMessagePayload>();
        _logger.LogInformation("Device name = {value}", deviceMessagePayload.Name);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _logger.LogInformation($"{nameof(HandlerForDeviceNumber2)} disposed.");;
    }
}