using Microsoft.Extensions.DependencyInjection;
using Mqtt.Library.Client;
using Mqtt.Library.Client.Configuration;

namespace Mqtt.Library.Processing;

public static class ServiceProviderExtensions
{
    public static void UseMqttMessageReceivedHandler<TMessagingClientOptions>(this IServiceProvider serviceProvider)
        where TMessagingClientOptions : IMqttMessagingClientOptions
    {
        var mqttMessagingClient = serviceProvider.GetRequiredService<IMqttMessagingClient<TMessagingClientOptions>>();
        var handler = serviceProvider.GetRequiredService<MqttReceivedMessageHandler>();
        mqttMessagingClient.UseMqttMessageReceivedHandler(handler);
    }
}