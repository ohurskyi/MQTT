using MessagingLibrary.Core.Handlers;
using MessagingLibrary.Core.Messages;
using MessagingLibrary.Core.Results;
using MqttLibrary.Examples.Pairing.Contracts.Payloads;

namespace DistributedConfiguration.Client.IntegrationEvents.PairedDevicesConfigurationChanged;

public class UpdateLocalConfigurationMessageHandler : MessageHandlerBase<PairedDevicesConfigurationChangedEventPayload>
{
    private readonly ILogger<UpdateLocalConfigurationMessageHandler> _logger;

    public UpdateLocalConfigurationMessageHandler(ILogger<UpdateLocalConfigurationMessageHandler> logger)
    {
        _logger = logger;
    }

    protected override async Task<IExecutionResult> HandleAsync(MessagingContext<PairedDevicesConfigurationChangedEventPayload> messagingContext)
    {
        var payload = messagingContext.Payload;
        var newConfiguration = payload.PairedDevices;
        _logger.LogInformation("New Configuration received with devices count: {value}. Update local configuration after distributed config change", newConfiguration.DeviceMacAddresses.Count);
        return await Task.FromResult(ExecutionResult.Ok());
    }
}