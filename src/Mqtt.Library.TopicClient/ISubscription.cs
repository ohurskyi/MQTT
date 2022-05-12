namespace Mqtt.Library.TopicClient;

public interface ISubscription
{
    Type MessageHandler { get; }
    string Topic { get; set; }
}