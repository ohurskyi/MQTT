using Mqtt.Library.Core;
using Mqtt.Library.Core.Extensions;
using Mqtt.Library.Core.Messages;
using Mqtt.Library.Test.Payloads;

namespace Mqtt.Library.Test.Handlers;

public class HandlerForDeviceNumber1 : MessageHandlerBase<DeviceMessagePayload>
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

    protected override Task HandleAsync(DeviceMessagePayload payload)
    {
        _logger.LogInformation("Handler {handler} received message", nameof(HandlerForDeviceNumber1));
        _logger.LogInformation("Device name = {value}", payload.Name);
        return Task.CompletedTask;
    }
}