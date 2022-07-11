using MessagingLibrary.TopicClient.Mqtt.Definitions;
using Mqtt.Library.Client.Infrastructure;

namespace DistributedConfiguration.Domain.Listeners;

public class ThisDomainConsumerDefinitionProvider : IConsumerDefinitionProvider<InfrastructureMqttMessagingClientOptions>
{
    public IEnumerable<IConsumerDefinition> ConsumerDefinitions()
    {
        return new List<IConsumerDefinition>
        {
            new DistributedConfigurationConsumerDefinition()
        };
    }
}