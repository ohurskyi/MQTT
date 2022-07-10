using MessagingLibrary.Client.Mqtt.Configuration;
using MessagingLibrary.TopicClient.Mqtt.Definitions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MessagingLibrary.TopicClient.Mqtt;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMqttTopicClient<TMessagingClientOptions>(this IServiceCollection serviceCollection) 
        where TMessagingClientOptions: class, IMqttMessagingClientOptions
    {
        serviceCollection.TryAddSingleton<IMqttTopicClient<TMessagingClientOptions>, MqttTopicClient<TMessagingClientOptions>>();
        return serviceCollection;
    }
    
    public static IServiceCollection AddConsumerDefinition<TConsumer, TMessagingClientOptions>(this IServiceCollection serviceCollection) 
        where TConsumer : class, IConsumerDefinition<TMessagingClientOptions>
        where TMessagingClientOptions: class, IMqttMessagingClientOptions
    {
        return serviceCollection.AddSingleton<IConsumerDefinition<TMessagingClientOptions>, TConsumer>();
    }

    public static IServiceCollection AddConsumerListenerHostedService<TMessagingClientOptions>(this IServiceCollection serviceCollection) 
        where TMessagingClientOptions: class, IMqttMessagingClientOptions
    {
        return serviceCollection.AddHostedService<ConsumerListener<TMessagingClientOptions>>();
    }
}