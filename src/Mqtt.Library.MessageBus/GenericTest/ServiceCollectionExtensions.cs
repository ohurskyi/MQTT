using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Mqtt.Library.Client.Configuration;

namespace Mqtt.Library.MessageBus.GenericTest;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMqttMessageBusGen<TMessagingClientOptions>(this IServiceCollection serviceCollection) where TMessagingClientOptions: IMqttMessagingClientOptions
    {
        serviceCollection.TryAddSingleton<MqttMessageBusGen<TMessagingClientOptions>>();
        return serviceCollection;
    }
}