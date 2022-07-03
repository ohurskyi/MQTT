using DistributedConfiguration.Contracts.Payloads;
using DistributedConfiguration.Contracts.Topics;
using MessagingLibrary.Core.Handlers;
using MessagingLibrary.Core.Messages;
using MessagingLibrary.Core.Results;
using Microsoft.Extensions.Logging;

namespace DistributedConfiguration.Domain.Handlers;

public class PairDeviceMessageHandler : MessageHandlerBase<PairDeviceContract>
{
    private readonly ILogger<PairDeviceMessageHandler> _logger;

    public PairDeviceMessageHandler(ILogger<PairDeviceMessageHandler> logger)
    {
        _logger = logger;
    }

    protected override async Task<IExecutionResult> HandleAsync(MessagingContext<PairDeviceContract> messagingContext)
    {
        var payload = messagingContext.Payload;
        
        _logger.LogInformation("Paired with device {value}", payload.MacAddress);

        var eventPayload = new PairedDevicesConfigurationChangedEventContract
        {
            PairedDevices = new PairedDevices {DeviceMacAddresses = new List<string> {payload.MacAddress}},
        };
        
        var integrationEventResult = new IntegrationEventResult(eventPayload, TopicConstants.CurrentConfiguration);

        return await Task.FromResult(integrationEventResult);
    }
}