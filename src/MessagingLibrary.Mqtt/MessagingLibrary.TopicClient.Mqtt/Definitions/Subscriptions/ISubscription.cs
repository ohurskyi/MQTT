namespace MessagingLibrary.TopicClient.Mqtt.Definitions.Subscriptions;

public interface ISubscription
{
    Type HandlerType { get; }
    string Topic { get;}
}