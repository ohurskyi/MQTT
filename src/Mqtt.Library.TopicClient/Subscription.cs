using Mqtt.Library.Core;
using Mqtt.Library.Core.Handlers;

namespace Mqtt.Library.TopicClient;

public class Subscription<T> : ISubscription
    where T: IMessageHandler
{
    public Subscription(string topic)
    {
        HandlerType = typeof(T);
        Topic = topic;
    }

    public Type HandlerType { get; }
    public string Topic { get; }
}