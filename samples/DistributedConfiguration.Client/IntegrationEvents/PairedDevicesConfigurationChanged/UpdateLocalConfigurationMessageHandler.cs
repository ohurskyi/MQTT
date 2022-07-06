using DistributedConfiguration.Contracts.Payloads;
using MessagingLibrary.Core.Handlers;
using MessagingLibrary.Core.Messages;
using MessagingLibrary.Core.Results;

namespace DistributedConfiguration.Client.IntegrationEvents.PairedDevicesConfigurationChanged;

public class UpdateLocalConfigurationMessageHandler : MessageHandlerBase<PairedDevicesConfigurationChangedEventContract>
{
    private readonly ILogger<UpdateLocalConfigurationMessageHandler> _logger;

    public UpdateLocalConfigurationMessageHandler(ILogger<UpdateLocalConfigurationMessageHandler> logger)
    {
        _logger = logger;
    }

    protected override async Task<IExecutionResult> HandleAsync(MessagingContext<PairedDevicesConfigurationChangedEventContract> messagingContext)
    {
        var payload = messagingContext.Payload;
        var newConfiguration = payload.PairedDevicesModel;
        _logger.LogInformation("New Configuration received with devices count: {value}. Update local configuration after distributed config change", newConfiguration.Devices.Count);
        return await Task.FromResult(ExecutionResult.Ok());
    }
}