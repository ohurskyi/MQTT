using System.Threading.Tasks;
using MessagingLibrary.Core.Handlers;
using MessagingLibrary.Core.Messages;
using MessagingLibrary.Core.Results;
using Microsoft.Extensions.Logging;
using Mqtt.Library.Unit.Payloads;

namespace Mqtt.Library.Unit.Handlers;

public class HandlerForDeviceNumber2 :  MessageHandlerBase<DeviceMessagePayload>
{
    private readonly ILogger<HandlerForDeviceNumber2> _logger;

    public HandlerForDeviceNumber2(ILogger<HandlerForDeviceNumber2> logger)
    {
        _logger = logger;
    }

    protected override async Task<IExecutionResult> HandleAsync(MessagingContext<DeviceMessagePayload> messagingContext)
    {
        _logger.LogInformation("Handler {handler} received message from topic = {value}", nameof(HandlerForDeviceNumber2), messagingContext.Topic);
        var deviceMessagePayload = messagingContext.Payload;
        _logger.LogInformation("Device name = {value}", deviceMessagePayload.Name);
        return await Task.FromResult(ExecutionResult.Ok());
    }
}