using MessagingLibrary.Client.Mqtt.Configuration;
using MessagingLibrary.Core.Clients;

namespace MessagingLibrary.TopicClient.Mqtt.Definitions.Consumers;

public class DefinitionListener<TMessagingClientOptions> : IDefinitionListener
    where TMessagingClientOptions : class, IMqttMessagingClientOptions
{
    private readonly IEnumerable<IConsumerDefinition> _consumerDefinitions;
    private readonly ITopicClient<TMessagingClientOptions> _topicClient;

    public DefinitionListener(IEnumerable<IConsumerDefinition> consumerDefinitions, ITopicClient<TMessagingClientOptions> topicClient)
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