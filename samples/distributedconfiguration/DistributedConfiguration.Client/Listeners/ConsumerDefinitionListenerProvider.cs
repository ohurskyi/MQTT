using MessagingLibrary.Core.Clients;
using MessagingLibrary.TopicClient.Mqtt.Definitions.Consumers;
using Mqtt.Library.Client.Infrastructure;

namespace DistributedConfiguration.Client.Listeners;

public class ConsumerDefinitionListenerProvider : IConsumerDefinitionListenerProvider
{
    private readonly ITopicClient<InfrastructureMqttMessagingClientOptions> _topicClient;

    public ConsumerDefinitionListenerProvider(ITopicClient<InfrastructureMqttMessagingClientOptions> topicClient)
    {
        _topicClient = topicClient;
    }

    public IEnumerable<IConsumerListener> Listeners => new List<IConsumerListener>
    {
        new ConsumerListener<InfrastructureMqttMessagingClientOptions>(new PairedDevicesDefinitionProvider().Definitions, _topicClient)
    };
}