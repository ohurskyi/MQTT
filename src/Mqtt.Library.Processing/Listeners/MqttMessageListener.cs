using Microsoft.Extensions.Hosting;
using Mqtt.Library.Client.Configuration;
using Mqtt.Library.TopicClient;

namespace Mqtt.Library.Processing.Listeners;

public class MqttMessageListener<TMessagingClientOptions> : IHostedService
    where TMessagingClientOptions : IMqttMessagingClientOptions
{
    private readonly IMqttStartupListener<TMessagingClientOptions> _mqttStartupListener;

    private ISubscription[] _subscriptions;
    
    public MqttMessageListener(IMqttStartupListener<TMessagingClientOptions> mqttStartupListener)
    {
        _mqttStartupListener = mqttStartupListener;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        _subscriptions =  await Task.WhenAll(_mqttStartupListener.DefineSubscriptions());
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}