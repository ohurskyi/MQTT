using MessagingLibrary.Core.Definitions.Subscriptions;

namespace MessagingLibrary.TopicClient.Mqtt.Definitions.Consumers;

public interface IConsumerDefinition
{
    IEnumerable<ISubscription> Definitions();
}