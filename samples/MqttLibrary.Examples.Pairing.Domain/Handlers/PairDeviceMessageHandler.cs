using Microsoft.Extensions.Logging;
using Mqtt.Library.Core;
using Mqtt.Library.Core.Results;
using MqttLibrary.Examples.Pairing.Contracts.Payloads;

namespace MqttLibrary.Examples.Pairing.Domain.Handlers;

public class PairDeviceMessageHandler : MessageHandlerBase<PairDevicePayload>
{
    private readonly ILogger<PairDeviceMessageHandler> _logger;

    public PairDeviceMessageHandler(ILogger<PairDeviceMessageHandler> logger)
    {
        _logger = logger;
    }

    protected override async Task<IExecutionResult> HandleAsync(PairDevicePayload payload)
    {
        _logger.LogInformation("Paired with device {value}", payload.MacAddress);
        return await Task.FromResult(ExecutionResult.Ok());
    }
}