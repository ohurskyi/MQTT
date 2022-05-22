using Microsoft.Extensions.Logging;
using Mqtt.Library.Core;
using Mqtt.Library.Core.Extensions;
using Mqtt.Library.Core.Messages;
using MqttLibrary.Examples.Contracts.Payloads;

namespace MqttLibrary.Examples.Domain.Handlers;

public class HandlerForAllDeviceNumbers : IMessageHandler, IDisposable
{
    private readonly ILogger<HandlerForAllDeviceNumbers> _logger;

    public HandlerForAllDeviceNumbers(ILogger<HandlerForAllDeviceNumbers> logger)
    {
        _logger = logger;
    }

    public async Task Handle(IMessage message)
    {
        _logger.LogInformation("Handler {handler} received message from topic = {value}", nameof(HandlerForAllDeviceNumbers), message.Topic);
        var deviceMessagePayload = message.Payload.MessagePayloadFromJson<DeviceMessagePayload>();
        _logger.LogInformation("Device name = {value}", deviceMessagePayload.Name);
        await Task.Delay(TimeSpan.FromSeconds(1));
    }

    public void Dispose()
    {
        _logger.LogInformation($"{nameof(HandlerForAllDeviceNumbers)} disposed.");
    }
}