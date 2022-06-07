using Microsoft.Extensions.Logging;
using Mqtt.Library.Core;
using Mqtt.Library.Core.Extensions;
using Mqtt.Library.Core.Handlers;
using Mqtt.Library.Core.Messages;
using Mqtt.Library.Core.Results;
using MqttLibrary.Examples.Contracts.Payloads;

namespace MqttLibrary.Examples.Domain.Handlers;

public class HandlerForDeviceNumber2 : IMessageHandler, IDisposable
{
    private readonly ILogger<HandlerForDeviceNumber2> _logger;

    public HandlerForDeviceNumber2(ILogger<HandlerForDeviceNumber2> logger)
    {
        _logger = logger;
    }

    public async Task<IExecutionResult> Handle(IMessage message)
    {
        _logger.LogInformation("Handler {handler} received message from topic = {value}", nameof(HandlerForDeviceNumber2), message.Topic);
        var deviceMessagePayload = message.Payload.MessagePayloadFromJson<DeviceMessagePayload>();
        _logger.LogInformation("Device name = {value}", deviceMessagePayload.Name);
        return await Task.FromResult(ExecutionResult.Ok());
    }

    public void Dispose()
    {
        _logger.LogInformation($"{nameof(HandlerForDeviceNumber2)} disposed.");;
    }
}