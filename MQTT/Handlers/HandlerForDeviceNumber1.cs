using Mqtt.Library.Core;
using Mqtt.Library.Test.Payloads;

namespace Mqtt.Library.Test.Handlers;

public class HandlerForDeviceNumber1 : MessageHandlerBase<DeviceMessagePayload>
{
    private readonly ILogger<HandlerForDeviceNumber1> _logger;

    public HandlerForDeviceNumber1(ILogger<HandlerForDeviceNumber1> logger)
    {
        _logger = logger;
    }

    protected override Task HandleAsync(DeviceMessagePayload payload)
    {
        _logger.LogInformation("Handler {handler} received message", nameof(HandlerForDeviceNumber1));
        _logger.LogInformation("Device name = {value}", payload.Name);
        return Task.CompletedTask;
    }
}

public class HandlerForDeviceNumber1DevConfig : MessageHandlerBase<DeviceMessagePayload>
{
    private readonly ILogger<HandlerForDeviceNumber1DevConfig> _logger;

    public HandlerForDeviceNumber1DevConfig(ILogger<HandlerForDeviceNumber1DevConfig> logger)
    {
        _logger = logger;
    }

    protected override Task HandleAsync(DeviceMessagePayload payload)
    {
        _logger.LogInformation("Handler {handler} received message", nameof(HandlerForDeviceNumber1DevConfig));
        _logger.LogInformation("Device name = {value}", payload.Name);
        return Task.CompletedTask;
    }
}