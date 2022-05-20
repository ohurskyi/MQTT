using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Mqtt.Library.Client.Configuration;

namespace Mqtt.Library.TopicClient;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMqttTopicClient<TMessagingClientOptions>(this IServiceCollection serviceCollection) 
        where TMessagingClientOptions: class, IMqttMessagingClientOptions
    {
        serviceCollection.TryAddSingleton<IMqttTopicClient<TMessagingClientOptions>, MqttTopicClient<TMessagingClientOptions>>();
        return serviceCollection;
    }
}