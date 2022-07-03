using DistributedConfiguration.Contracts.Payloads;
using MessagingLibrary.Core.Handlers;
using MessagingLibrary.Core.Messages;
using MessagingLibrary.Core.Results;

namespace DistributedConfiguration.Client.IntegrationEvents.PairedDevicesConfigurationChanged;

public class NotifyUsersMessageHandler : MessageHandlerBase<PairedDevicesConfigurationChangedEventContract>
{
    private readonly ILogger<NotifyUsersMessageHandler> _logger;

    public NotifyUsersMessageHandler(ILogger<NotifyUsersMessageHandler> logger)
    {
        _logger = logger;
    }

    protected override async Task<IExecutionResult> HandleAsync(MessagingContext<PairedDevicesConfigurationChangedEventContract> messagingContext)
    {
        _logger.LogInformation("Notify users about distributed config change");
        return await Task.FromResult(ExecutionResult.Ok());
    }
}