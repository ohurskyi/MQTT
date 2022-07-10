using MessagingLibrary.Client.Mqtt.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MessagingLibrary.TopicClient.Mqtt.Definitions;

public class ConsumerListener<TMessagingClientOptions> : IHostedService
    where TMessagingClientOptions : class, IMqttMessagingClientOptions
{
    private readonly IEnumerable<IConsumerDefinition<TMessagingClientOptions>> _consumerDefinitions;
    private readonly IMqttTopicClient<TMessagingClientOptions> _topicClient;
    private readonly ILogger<ConsumerListener<TMessagingClientOptions>> _logger;
    
    private ISubscription[] _subscriptions = Array.Empty<ISubscription>();

    public ConsumerListener(IEnumerable<IConsumerDefinition<TMessagingClientOptions>> consumerDefinitions, IMqttTopicClient<TMessagingClientOptions> topicClient, ILogger<ConsumerListener<TMessagingClientOptions>> logger)
    {
        _consumerDefinitions = consumerDefinitions;
        _topicClient = topicClient;
        _logger = logger;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _subscriptions = await Task.WhenAll(_consumerDefinitions.SelectMany(s => s.Subscriptions(_topicClient)));
        _logger.LogInformation("{count} subscriptions created.", _subscriptions.Length);
        _logger.LogInformation("Subscribed topics {value}", _subscriptions.Select(s => s.Topic).Distinct());
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await Task.WhenAll(_subscriptions.Select(subscription => _topicClient.Unsubscribe(subscription)));
        _logger.LogInformation("{count} subscriptions removed.", _subscriptions.Length);
        _logger.LogInformation("Unsubscribed from topics {value}", _subscriptions.Select(s => s.Topic).Distinct());
    }
}