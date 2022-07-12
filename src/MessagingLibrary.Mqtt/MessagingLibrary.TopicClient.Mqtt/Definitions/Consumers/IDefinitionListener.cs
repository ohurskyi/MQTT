namespace MessagingLibrary.TopicClient.Mqtt.Definitions.Consumers;

public interface IDefinitionListener
{
    Task StartListening();
    Task StopListening();
}