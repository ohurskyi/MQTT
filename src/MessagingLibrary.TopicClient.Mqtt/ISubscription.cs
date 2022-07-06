namespace MessagingLibrary.TopicClient.Mqtt;

public interface ISubscription
{
    Type HandlerType { get; }
    string Topic { get;}
}