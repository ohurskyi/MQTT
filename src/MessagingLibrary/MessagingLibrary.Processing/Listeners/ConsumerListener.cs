using MessagingLibrary.Core.Clients;
using MessagingLibrary.Core.Configuration;
using MessagingLibrary.Core.Definitions.Consumers;

namespace MessagingLibrary.Processing.Listeners;

public class ConsumerListener<TMessagingClientOptions> : IConsumerListener
    where TMessagingClientOptions : class, IMessagingClientOptions
{
    private readonly IEnumerable<IConsumerDefinition> _consumerDefinitions;
    private readonly ITopicClient<TMessagingClientOptions> _topicClient;

    public ConsumerListener(IEnumerable<IConsumerDefinition> consumerDefinitions, ITopicClient<TMessagingClientOptions> topicClient)
    {
        _consumerDefinitions = consumerDefinitions;
        _topicClient = topicClient;
    }

    public async Task StartListening()
    {
        var definitions = _consumerDefinitions.SelectMany(c => c.Definitions());
        await Task.WhenAll(definitions.Select(d => _topicClient.Subscribe(d)));
    }
    
    public async Task StopListening()
    {
        var definitions = _consumerDefinitions.SelectMany(c => c.Definitions());
        await Task.WhenAll(definitions.Select(d => _topicClient.Unsubscribe(d)));
    }
}