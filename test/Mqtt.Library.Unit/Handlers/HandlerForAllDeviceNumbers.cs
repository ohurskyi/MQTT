using System;
using System.Threading.Tasks;
using MessagingLibrary.Core.Handlers;
using MessagingLibrary.Core.Messages;
using MessagingLibrary.Core.Results;
using Microsoft.Extensions.Logging;
using Mqtt.Library.Unit.Payloads;

namespace Mqtt.Library.Unit.Handlers;

public class HandlerForAllDeviceNumbers : MessageHandlerBase<DeviceMessagePayload>
{
    private readonly ILogger<HandlerForAllDeviceNumbers> _logger;

    public HandlerForAllDeviceNumbers(ILogger<HandlerForAllDeviceNumbers> logger)
    {
        _logger = logger;
    }

    protected override async Task<IExecutionResult> HandleAsync(MessagingContext<DeviceMessagePayload> messagingContext)
    {
        _logger.LogInformation("Handler {handler} received message from topic = {value}", nameof(HandlerForAllDeviceNumbers), messagingContext.Topic);
        var deviceMessagePayload = messagingContext.Payload;
        _logger.LogInformation("Device name = {value}", deviceMessagePayload.Name);
        await Task.Delay(TimeSpan.FromSeconds(1));
        return ExecutionResult.Ok();
    }
}