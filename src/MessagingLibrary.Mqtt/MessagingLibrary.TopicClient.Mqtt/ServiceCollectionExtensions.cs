using MessagingLibrary.Client.Mqtt.Configuration;
using MessagingLibrary.Core.Clients;
using MessagingLibrary.TopicClient.Mqtt.Definitions.Consumers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MessagingLibrary.TopicClient.Mqtt;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMqttTopicClient<TMessagingClientOptions>(this IServiceCollection serviceCollection) 
        where TMessagingClientOptions: class, IMqttMessagingClientOptions
    {
        serviceCollection.TryAddSingleton<ITopicClient<TMessagingClientOptions>, MqttTopicClient<TMessagingClientOptions>>();
        return serviceCollection;
    }
    
    public static IServiceCollection AddConsumerDefinitionProvider<TConsumerDefinitionProvider>(this IServiceCollection serviceCollection)
        where TConsumerDefinitionProvider: class, IConsumerDefinitionProvider
    {
        serviceCollection.TryAddSingleton<IConsumerDefinitionProvider, TConsumerDefinitionProvider>();
        return serviceCollection;
    }

    public static IServiceCollection AddConsumerListener(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddHostedService<ConsumerListener>();
        return serviceCollection;
    }
}