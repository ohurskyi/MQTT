using MessagingLibrary.Client.Mqtt.Configuration;
using MessagingLibrary.Core.Clients;
using Microsoft.Extensions.Hosting;

namespace MessagingLibrary.TopicClient.Mqtt.Definitions.Consumers;

public class ConsumerListener<TMessagingClientOptions> : IHostedService
    where TMessagingClientOptions : class, IMqttMessagingClientOptions
{
    private readonly IConsumerDefinitionProvider<TMessagingClientOptions> _consumerDefinitionProvider;
    private readonly ITopicClient<TMessagingClientOptions> _topicClient;

    public ConsumerListener(IConsumerDefinitionProvider<TMessagingClientOptions> consumerDefinitionProvider, ITopicClient<TMessagingClientOptions> topicClient)
    {
        _consumerDefinitionProvider = consumerDefinitionProvider;
        _topicClient = topicClient;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        var consumerDefinitions = _consumerDefinitionProvider.Definitions;
        var subs = consumerDefinitions.SelectMany(c => c.Definitions()).ToList();
        await Task.WhenAll(subs.Select(s => _topicClient.Subscribe(s)));
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        var consumerDefinitions = _consumerDefinitionProvider.Definitions;
        var subs = consumerDefinitions.SelectMany(c => c.Definitions()).ToList();
        await Task.WhenAll(subs.Select(s => _topicClient.Unsubscribe(s)));
    }
}