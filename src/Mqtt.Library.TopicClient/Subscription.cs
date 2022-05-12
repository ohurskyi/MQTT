using Mqtt.Library.Core;

namespace Mqtt.Library.TopicClient;

public class Subscription<T> : ISubscription 
    where T : IMessageHandler
{
    public Subscription(string topic)
    {
        MessageHandler = typeof(T);
        Topic = topic;
    }

    public Type MessageHandler { get; }
    public string Topic { get; set; }
}