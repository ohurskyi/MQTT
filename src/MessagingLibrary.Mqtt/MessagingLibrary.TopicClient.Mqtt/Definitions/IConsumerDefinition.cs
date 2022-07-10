using MessagingLibrary.Client.Mqtt.Configuration;

namespace MessagingLibrary.TopicClient.Mqtt.Definitions;

public interface IConsumerDefinition<TMessagingClientOptions> where TMessagingClientOptions : class, IMqttMessagingClientOptions
{
    IEnumerable<Task<ISubscription>> Subscriptions(IMqttTopicClient<TMessagingClientOptions> topicClient);
}