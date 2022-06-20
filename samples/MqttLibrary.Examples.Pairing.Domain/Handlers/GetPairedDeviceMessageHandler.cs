﻿using MessagingLibrary.Core.Handlers;
using MessagingLibrary.Core.Messages;
using MessagingLibrary.Core.Results;
using Microsoft.Extensions.Logging;
using MqttLibrary.Examples.Pairing.Contracts.Payloads;

namespace MqttLibrary.Examples.Pairing.Domain.Handlers;

public class GetPairedDeviceMessageHandler : MessageHandlerBase<GetPairedDevicePayload>
{
    private readonly ILogger<PairDeviceMessageHandler> _logger;

    public GetPairedDeviceMessageHandler(ILogger<PairDeviceMessageHandler> logger)
    {
        _logger = logger;
    }

    protected override async Task<IExecutionResult> HandleAsync(MessagingContext<GetPairedDevicePayload> messagingContext)
    {
        var payload = messagingContext.Payload;
        
        _logger.LogInformation("Get device with id {value}", payload.DeviceId);

        var response = new GetPairedDeviceResponse {
            DeviceId = payload.DeviceId, 
            DeviceName = $"{payload.DeviceId}-{Guid.NewGuid()}-D",
        };

        var result = new ReplyResult(response, messagingContext.ReplyTopic, messagingContext.CorrelationId);
        
        return await Task.FromResult(result);
    }
}