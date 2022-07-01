using Microsoft.Extensions.DependencyInjection;
using Mqtt.Library.Client;
using Mqtt.Library.Client.Configuration;

namespace MessagingLibrary.Processing.Mqtt;

public static class ServiceProviderExtensions
{
    public static void UseMqttMessageReceivedHandler<TMessagingClientOptions>(this IServiceProvider serviceProvider)
        where TMessagingClientOptions : class, IMqttMessagingClientOptions
    {
        var mqttMessagingClient = serviceProvider.GetRequiredService<IMqttMessagingClient<TMessagingClientOptions>>();
        var handler = serviceProvider.GetRequiredService<MqttReceivedMessageHandler<TMessagingClientOptions>>();
        mqttMessagingClient.UseMqttMessageReceivedHandler(handler);
    }
}