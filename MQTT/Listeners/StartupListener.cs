using Mqtt.Library.Core;
using Mqtt.Library.Test.ClientOptions;
using Mqtt.Library.TopicClient;

namespace Mqtt.Library.Test.Listeners;

public abstract class StartupListener : IHostedService
{
    protected readonly IMqttTopicClient<LocalMqttMessagingClientOptions> TopicClient;

    protected StartupListener(IMqttTopicClient<LocalMqttMessagingClientOptions> topicClient)
    {
        TopicClient = topicClient;
    }

    protected abstract Task CreateSubscriptions();

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await CreateSubscriptions();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}