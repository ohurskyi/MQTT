using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Mqtt.Library.Client.Configuration;

namespace Mqtt.Library.TopicClient.GenericTest;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMqttTopicClientGen<TMessagingClientOptions>(this IServiceCollection serviceCollection) where TMessagingClientOptions: IMqttMessagingClientOptions
    {
        serviceCollection.TryAddSingleton<MqttTopicClientGen<TMessagingClientOptions>>();
        return serviceCollection;
    }
}