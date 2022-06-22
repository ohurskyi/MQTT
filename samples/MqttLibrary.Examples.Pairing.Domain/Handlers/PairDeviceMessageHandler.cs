using MessagingLibrary.Core.Handlers;
using MessagingLibrary.Core.Messages;
using MessagingLibrary.Core.Results;
using Microsoft.Extensions.Logging;
using MqttLibrary.Examples.Pairing.Contracts.Payloads;
using MqttLibrary.Examples.Pairing.Contracts.Topics;

namespace MqttLibrary.Examples.Pairing.Domain.Handlers;

public class PairDeviceMessageHandler : MessageHandlerBase<PairDevicePayload>
{
    private readonly ILogger<PairDeviceMessageHandler> _logger;

    public PairDeviceMessageHandler(ILogger<PairDeviceMessageHandler> logger)
    {
        _logger = logger;
    }

    protected override async Task<IExecutionResult> HandleAsync(MessagingContext<PairDevicePayload> messagingContext)
    {
        var payload = messagingContext.Payload;
        
        _logger.LogInformation("Paired with device {value}", payload.MacAddress);

        var eventPayload = new PairedDevicesConfigurationChangedEventPayload
        {
            PairedDevices = new PairedDevices {DeviceMacAddresses = new List<string> {payload.MacAddress}},
        };
        
        var integrationEventResult = new IntegrationEventResult(eventPayload, TopicConstants.CurrentConfiguration);

        return await Task.FromResult(integrationEventResult);
    }
}