using MessagingLibrary.Core.Handlers;
using MessagingLibrary.Core.Messages;
using MessagingLibrary.Core.Results;
using MqttLibrary.Examples.Pairing.Contracts.Payloads;

namespace DistributedConfiguration.Client.IntegrationEvents.PairedDevicesConfigurationChanged;

public class NotifyUsersMessageHandler : MessageHandlerBase<PairedDevicesConfigurationChangedEventPayload>
{
    private readonly ILogger<NotifyUsersMessageHandler> _logger;

    public NotifyUsersMessageHandler(ILogger<NotifyUsersMessageHandler> logger)
    {
        _logger = logger;
    }

    protected override async Task<IExecutionResult> HandleAsync(MessagingContext<PairedDevicesConfigurationChangedEventPayload> messagingContext)
    {
        _logger.LogInformation("Notify users about distributed config change");
        return await Task.FromResult(ExecutionResult.Ok());
    }
}