using Microsoft.Extensions.DependencyInjection;
using Mqtt.Library.Client.Configuration;

namespace Mqtt.Library.TopicClient;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMqttTopicClient<TMessagingClientOptions>(this IServiceCollection serviceCollection) where TMessagingClientOptions: IMqttMessagingClientOptions
    {
        serviceCollection.AddSingleton<IMqttTopicClient<TMessagingClientOptions>, MqttTopicClient<TMessagingClientOptions>>();
        return serviceCollection;
    }
}