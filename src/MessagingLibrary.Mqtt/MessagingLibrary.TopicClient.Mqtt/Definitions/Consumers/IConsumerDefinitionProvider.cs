using MessagingLibrary.Core.Definitions;
using MessagingLibrary.Core.Definitions.Consumers;

namespace MessagingLibrary.TopicClient.Mqtt.Definitions.Consumers;

public interface IConsumerDefinitionProvider
{
    IEnumerable<IConsumerDefinition> Definitions { get; }
}