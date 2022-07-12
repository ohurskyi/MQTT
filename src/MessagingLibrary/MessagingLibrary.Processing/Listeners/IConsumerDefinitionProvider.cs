using MessagingLibrary.Core.Definitions.Consumers;

namespace MessagingLibrary.Processing.Listeners;

public interface IConsumerDefinitionProvider
{
    IEnumerable<IConsumerDefinition> Definitions { get; }
}