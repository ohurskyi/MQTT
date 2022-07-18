using System.Collections.Concurrent;
using DistributedConfiguration.Contracts.Models;
using DistributedConfiguration.Contracts.Pairing;
using DistributedConfiguration.Contracts.Topics;
using MessagingLibrary.Core.Handlers;
using MessagingLibrary.Core.Messages;
using MessagingLibrary.Core.Results;
using Microsoft.Extensions.Logging;

namespace DistributedConfiguration.Domain.Handlers;

public class PairDeviceMessageHandler : MessageHandlerBase<PairDeviceContract>
{
    private readonly ILogger<PairDeviceMessageHandler> _logger;
    private static readonly ConcurrentDictionary<string, Device> _pairedDevicesStorage = new();

    public PairDeviceMessageHandler(ILogger<PairDeviceMessageHandler> logger)
    {
        _logger = logger;
    }

    protected override async Task<IExecutionResult> HandleAsync(MessagingContext<PairDeviceContract> messagingContext)
    {
        var payload = messagingContext.Payload;
        
        if (_pairedDevicesStorage.ContainsKey(payload.MacAddress))
        {
            return new SuccessfulResult();
        }
        
        _logger.LogInformation("Paired with device {value}", payload.MacAddress);

        _pairedDevicesStorage.TryAdd(payload.MacAddress, new Device { MacAddress = payload.MacAddress });

        var eventPayload = new PairedDevicesConfigurationChangedEventContract
        {
            PairedDevicesModel = new PairedDevicesModel {Devices = _pairedDevicesStorage.Values.ToList() }
        };
        
        var integrationEventResult = new IntegrationEventResult(eventPayload, DistributedConfigurationTopicConstants.CurrentConfiguration);

        return await Task.FromResult(integrationEventResult);
    }
}