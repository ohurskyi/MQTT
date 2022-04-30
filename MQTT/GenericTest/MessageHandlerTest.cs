using Mqtt.Library.Core;
using Mqtt.Library.Core.Messages;

namespace Mqtt.Library.Test.GenericTest;

public class MessageHandlerTest: IMessageHandler, IDisposable
{
    private readonly ILogger<MessageHandlerTest> _logger;

    public MessageHandlerTest(ILogger<MessageHandlerTest> logger)
    {
        _logger = logger;
    }

    public Task Handle(IMessage message)
    {
        _logger.LogInformation("Handler {handler} received message from topic = {value}", nameof(MessageHandlerTest), message.Topic);
        var payload = message.FromJson<TestMessagePayload>();
        return Task.CompletedTask;
    }
    
    public void Dispose()
    {
        _logger.LogInformation($"{nameof(MessageHandlerTest)} disposed.");
    }
}

public class TestMessagePayload : IMessagePayload
{
    public string Name { get; set; } = "Test";
}