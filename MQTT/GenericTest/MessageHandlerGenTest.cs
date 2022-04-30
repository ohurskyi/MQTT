using Mqtt.Library.Core.GenericTest;

namespace Mqtt.Library.Test.GenericTest;

public class MessageHandlerGenTest: IMessageHandlerGen
{
    public Task Handle(IMessage message)
    {
        var payload = message.FromJson<TestMessagePayload>();
        return Task.CompletedTask;
    }
}

public class TestMessagePayload : IMessagePayload
{
    public string Name { get; set; } = "Test";
}