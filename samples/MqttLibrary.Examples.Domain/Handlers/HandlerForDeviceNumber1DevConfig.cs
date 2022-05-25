using Microsoft.Extensions.Logging;
using Mqtt.Library.Core;
using Mqtt.Library.Core.Results;
using MqttLibrary.Examples.Contracts.Payloads;

namespace MqttLibrary.Examples.Domain.Handlers;

public class HandlerForDeviceNumber1DevConfig : MessageHandlerBase<DeviceMessagePayload>
{
    private readonly ILogger<HandlerForDeviceNumber1DevConfig> _logger;

    public HandlerForDeviceNumber1DevConfig(ILogger<HandlerForDeviceNumber1DevConfig> logger)
    {
        _logger = logger;
    }

    protected override async Task<IExecutionResult> HandleAsync(DeviceMessagePayload payload)
    {
        _logger.LogInformation("Handler {handler} received message", nameof(HandlerForDeviceNumber1DevConfig));
        _logger.LogInformation("Device name = {value}", payload.Name);
        return await Task.FromResult(ExecutionResult.Ok());
    }
}