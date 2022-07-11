using MessagingLibrary.Core.Handlers;

namespace MessagingLibrary.TopicClient.Mqtt.Definitions.Subscriptions;

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