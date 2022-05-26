using Microsoft.Extensions.Logging;
using Mqtt.Library.Client.Local;
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

        var eventPayload = new PairedDevicesConfigurationChangedEventPayload
        {
            PairedDevices = new PairedDevices {DeviceMacAddresses = new List<string> {payload.MacAddress}},
        };
        
        var integrationEventResult = IntegrationEventResult.CreateIntegrationEventResult<LocalMqttMessagingClientOptions>(eventPayload);
                
        return await Task.FromResult(integrationEventResult);
    }
}