namespace MessagingLibrary.TopicClient.Mqtt.Definitions.Consumers;

public interface IConsumerListener
{
    Task StartListening();
    Task StopListening();
}