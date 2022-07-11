using MessagingLibrary.Client.Mqtt.Configuration;
using MessagingLibrary.TopicClient.Mqtt;
using MessagingLibrary.TopicClient.Mqtt.Definitions.Subscriptions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MessagingLibrary.Processing.Mqtt.Listeners;

public abstract class BaseMqttStartupListener<TMessagingClientOptions> : IHostedService
    where TMessagingClientOptions : class, IMqttMessagingClientOptions
{
    private readonly ILogger<BaseMqttStartupListener<TMessagingClientOptions>> _logger;
    protected readonly IMqttTopicClient<TMessagingClientOptions> TopicClient;

    private ISubscription[] _subscriptions = Array.Empty<ISubscription>();

    protected BaseMqttStartupListener(IMqttTopicClient<TMessagingClientOptions> topicClient, ILogger<BaseMqttStartupListener<TMessagingClientOptions>> logger)
    {
        TopicClient = topicClient;
        _logger = logger;
    }

    protected abstract IEnumerable<Task<ISubscription>> Subscriptions();
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _subscriptions = await Task.WhenAll(Subscriptions());
        _logger.LogInformation("{count} subscriptions created.", _subscriptions.Length);
        _logger.LogInformation("Subscribed topics {value}", _subscriptions.Select(s => s.Topic).Distinct());
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await Task.WhenAll(_subscriptions.Select(subscription => TopicClient.Unsubscribe(subscription)));
        _logger.LogInformation("{count} subscriptions removed.", _subscriptions.Length);
        _logger.LogInformation("Unsubscribed from topics {value}", _subscriptions.Select(s => s.Topic).Distinct());
    }
}