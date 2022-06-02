﻿using Microsoft.Extensions.Logging;
using Mqtt.Library.Core;
using Mqtt.Library.Core.Extensions;
using Mqtt.Library.Core.Messages;
using Mqtt.Library.Core.Results;
using MqttLibrary.Examples.Pairing.Contracts.Payloads;

namespace MqttLibrary.Examples.Pairing.Domain.Handlers;

public class GetPairedDeviceMessageHandler : IMessageHandler
{
    private readonly ILogger<PairDeviceMessageHandler> _logger;

    public GetPairedDeviceMessageHandler(ILogger<PairDeviceMessageHandler> logger)
    {
        _logger = logger;
    }

    public async Task<IExecutionResult> Handle(IMessage message)
    {
        var payload = message.Payload.MessagePayloadFromJson<GetPairedDevicePayload>();
        
        _logger.LogInformation("Get device with id {value}", payload.DeviceId);
        
        var response = new {
            DeviceId = payload.DeviceId, 
            DeviceName = $"{payload.DeviceId}-{Guid.NewGuid()}-D",
            CorrelationId = message.CorrelationId,
            Topic = message.ReplyTopic
        };
        
        return await Task.FromResult(ExecutionResult.Ok());
    }
}