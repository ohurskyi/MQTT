using MessagingLibrary.Client.Mqtt;
using MessagingLibrary.Client.Mqtt.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mqtt.Library.Client;

namespace MessagingLibrary.Processing.Mqtt;

public static class ServiceProviderExtensions
{
    public static void UseMqttMessageReceivedHandler<TMessagingClientOptions>(this IServiceProvider serviceProvider)
        where TMessagingClientOptions : class, IMqttMessagingClientOptions
    {
        var mqttMessagingClient = serviceProvider.GetRequiredService<IMqttMessagingClient<TMessagingClientOptions>>();
        var handler = serviceProvider.GetRequiredService<MqttReceivedMessageHandler<TMessagingClientOptions>>();
        mqttMessagingClient.UseMqttMessageReceivedHandler(handler.HandleApplicationMessageReceivedAsync);
    }
}