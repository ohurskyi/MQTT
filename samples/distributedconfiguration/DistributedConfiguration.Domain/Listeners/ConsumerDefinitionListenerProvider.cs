using MessagingLibrary.Core.Clients;
using MessagingLibrary.TopicClient.Mqtt.Definitions.Consumers;
using Mqtt.Library.Client.Infrastructure;

namespace DistributedConfiguration.Domain.Listeners;

public class ConsumerDefinitionListenerProvider : IConsumerDefinitionListenerProvider
{
    private readonly ITopicClient<InfrastructureMqttMessagingClientOptions> _topicClient;

    public ConsumerDefinitionListenerProvider(ITopicClient<InfrastructureMqttMessagingClientOptions> topicClient)
    {
        _topicClient = topicClient;
    }

    public IEnumerable<IDefinitionListener> Listeners => new List<IDefinitionListener>
    {
        new DefinitionListener<InfrastructureMqttMessagingClientOptions>(new PairingConsumerDefinitionProvider().Definitions, _topicClient)
    };
}