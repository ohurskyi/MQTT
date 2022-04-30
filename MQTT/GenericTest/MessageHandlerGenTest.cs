using Mqtt.Library.Core.GenericTest;

namespace Mqtt.Library.Test.GenericTest;

public class MessageHandlerGenTest: IMessageHandlerGen, IDisposable
{
    private readonly ILogger<MessageHandlerGenTest> _logger;

    public MessageHandlerGenTest(ILogger<MessageHandlerGenTest> logger)
    {
        _logger = logger;
    }

    public Task Handle(IMessage message)
    {
        _logger.LogInformation("Handler {handler} received message from topic = {value}", nameof(MessageHandlerGenTest), message.Topic);
        var payload = message.FromJson<TestMessagePayload>();
        return Task.CompletedTask;
    }
    
    public void Dispose()
    {
        _logger.LogInformation($"{nameof(MessageHandlerGenTest)} disposed.");
    }
}

public class TestMessagePayload : IMessagePayload
{
    public string Name { get; set; } = "Test";
}