namespace Mqtt.Library.TopicClient;

public interface ISubscription
{
    Type HandlerType { get; }
    string Topic { get;}
}