using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Mqtt.Library.Client.Configuration;
using Mqtt.Library.TopicClient;

namespace Mqtt.Library.Processing.Listeners;

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

    protected abstract IEnumerable<Task<ISubscription>> DefineSubscriptions();
    
    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _subscriptions = await Task.WhenAll(DefineSubscriptions());
        _logger.LogInformation("Created subscriptions {count}", _subscriptions.Length);
    }

    public async Task StopAsync(CancellationToken cancellationToken)
    {
        await Task.WhenAll(_subscriptions.Select(subscription => TopicClient.Unsubscribe(subscription)));
    }
}