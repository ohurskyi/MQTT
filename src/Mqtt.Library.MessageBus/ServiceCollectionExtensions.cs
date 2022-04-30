using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Mqtt.Library.Client.Configuration;

namespace Mqtt.Library.MessageBus;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMqttMessageBus<TMessagingClientOptions>(this IServiceCollection serviceCollection) where TMessagingClientOptions: IMqttMessagingClientOptions
    {
        serviceCollection.TryAddSingleton<IMqttMessageBus<TMessagingClientOptions>, MqttMessageBus<TMessagingClientOptions>>();
        return serviceCollection;
    }
}