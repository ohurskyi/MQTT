using MessagingLibrary.Core.Clients;
using MessagingLibrary.Processing.Mqtt.Clients;
using MessagingLibrary.Processing.Mqtt.Configuration.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MessagingLibrary.Processing.Mqtt.Configuration.DependencyInjection;

public static class MqttMessageBusServiceCollectionExtensions
{
    public static IServiceCollection AddMqttMessageBus<TMessagingClientOptions>(this IServiceCollection serviceCollection) where TMessagingClientOptions: IMqttMessagingClientOptions
    {
        serviceCollection.TryAddSingleton<IMessageBus<TMessagingClientOptions>, MqttMessageBus<TMessagingClientOptions>>();
        return serviceCollection;
    }
}