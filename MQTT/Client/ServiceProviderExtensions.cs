using Mqtt.Library.Client;
using Mqtt.Library.Client.Configuration;
using Mqtt.Library.Test.Core;

namespace Mqtt.Library.Test.Client;

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