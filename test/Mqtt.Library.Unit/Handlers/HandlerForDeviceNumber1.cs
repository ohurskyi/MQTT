using System.Threading.Tasks;
using MessagingLibrary.Core.Handlers;
using MessagingLibrary.Core.Messages;
using MessagingLibrary.Core.Results;
using Microsoft.Extensions.Logging;
using Mqtt.Library.Unit.Payloads;

namespace Mqtt.Library.Unit.Handlers;

public class HandlerForDeviceNumber1 : MessageHandlerBase<DeviceMessagePayload>
{
    private readonly ILogger<HandlerForDeviceNumber1> _logger;

    public HandlerForDeviceNumber1(ILogger<HandlerForDeviceNumber1> logger)
    {
        _logger = logger;
    }
    
    protected override async Task<IExecutionResult> HandleAsync(MessagingContext<DeviceMessagePayload> messagingContext)
    {
        var payload = messagingContext.Payload;
        _logger.LogInformation("Handler {handler} received message", nameof(HandlerForDeviceNumber1));
        _logger.LogInformation("Device name = {value}", payload.Name);
        return await Task.FromResult(ExecutionResult.Fail("fail"));
    }
}