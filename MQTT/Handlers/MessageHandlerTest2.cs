using Mqtt.Library.Processing;
using MQTTnet;

namespace Mqtt.Library.Test.Handlers;

public class MessageHandlerTest2 : IMessageHandler, IDisposable
{
    private readonly ILogger<MessageHandlerTest2> _logger;

    public MessageHandlerTest2(ILogger<MessageHandlerTest2> logger)
    {
        _logger = logger;
    }

    public Task Handle(MqttApplicationMessage mqttApplicationMessage)
    {
        _logger.LogInformation("Handler {handler} received message from topic = {value}", nameof(MessageHandlerTest2), mqttApplicationMessage.Topic);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        _logger.LogInformation($"{nameof(MessageHandlerTest2)} disposed.");;
    }
}