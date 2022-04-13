using Mqtt.Library.Core;
using Mqtt.Library.Processing;
using MQTTnet;

namespace Mqtt.Library.Test.Handlers;

public class MessageHandlerTest : IMessageHandler, IDisposable
{
    private readonly ILogger<MessageHandlerTest> _logger;

    public MessageHandlerTest(ILogger<MessageHandlerTest> logger)
    {
        _logger = logger;
    }

    public Task Handle(MqttApplicationMessage mqttApplicationMessage)
    {
        _logger.LogInformation("Handler {handler} received message from topic = {value}", nameof(MessageHandlerTest), mqttApplicationMessage.Topic);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _logger.LogInformation($"{nameof(MessageHandlerTest)} disposed.");;
    }
}