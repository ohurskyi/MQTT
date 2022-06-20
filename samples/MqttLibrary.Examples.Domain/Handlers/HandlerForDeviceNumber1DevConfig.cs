using MessagingLibrary.Core.Handlers;
using MessagingLibrary.Core.Messages;
using MessagingLibrary.Core.Results;
using Microsoft.Extensions.Logging;
using MqttLibrary.Examples.Contracts.Payloads;

namespace MqttLibrary.Examples.Domain.Handlers;

public class HandlerForDeviceNumber1DevConfig : MessageHandlerBase<DeviceMessagePayload>
{
    private readonly ILogger<HandlerForDeviceNumber1DevConfig> _logger;

    public HandlerForDeviceNumber1DevConfig(ILogger<HandlerForDeviceNumber1DevConfig> logger)
    {
        _logger = logger;
    }

    protected override async Task<IExecutionResult> HandleAsync(MessagingContext<DeviceMessagePayload> messagingContext)
    {
        var payload = messagingContext.Payload;
        _logger.LogInformation("Handler {handler} received message", nameof(HandlerForDeviceNumber1DevConfig));
        _logger.LogInformation("Device name = {value}", payload.Name);
        return await Task.FromResult(ExecutionResult.Ok());
    }
}