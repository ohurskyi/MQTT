using Microsoft.Extensions.Hosting;
using Mqtt.Library.Client.Configuration;

namespace Mqtt.Library.Processing.Listeners;

public class MqttMessageListener<TMessagingClientOptions> : IHostedService
    where TMessagingClientOptions : IMqttMessagingClientOptions
{
    private readonly IMqttStartupListener<TMessagingClientOptions> _mqttStartupListener;

    public MqttMessageListener(IMqttStartupListener<TMessagingClientOptions> mqttStartupListener)
    {
        _mqttStartupListener = mqttStartupListener;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await _mqttStartupListener.CreateSubscriptions();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}