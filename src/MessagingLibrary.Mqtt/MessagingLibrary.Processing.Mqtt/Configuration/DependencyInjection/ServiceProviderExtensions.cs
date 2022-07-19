using MessagingLibrary.Processing.Mqtt.Clients;
using MessagingLibrary.Processing.Mqtt.Configuration.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MessagingLibrary.Processing.Mqtt.Configuration.DependencyInjection;

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