using Microsoft.Extensions.DependencyInjection.Extensions;
using Mqtt.Library.Client.Configuration;
using Mqtt.Library.Processing;
using Mqtt.Library.Test.Client;

namespace MessagingClient.Mqtt;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddTopicClient<TMessagingClientOptions>(
        this IServiceCollection serviceCollection)
        where TMessagingClientOptions: IMqttMessagingClientOptions
    {
        serviceCollection.AddSingleton<ITopicClient<TMessagingClientOptions>, TopicClient<TMessagingClientOptions>>();
        return serviceCollection;
    }
        
    public static IServiceCollection AddMqttMessageBus<TMessagingClientOptions>(
        this IServiceCollection serviceCollection)
        where TMessagingClientOptions: IMqttMessagingClientOptions
    {
        serviceCollection.AddSingleton<IMqttMessageBus<TMessagingClientOptions>, MqttMqttMessageBus<TMessagingClientOptions>>();
        return serviceCollection;
    }

    public static IServiceCollection AddMqttApplicationMessageReceivedHandler(this IServiceCollection serviceCollection)
    {
        serviceCollection.TryAddSingleton<MqttReceivedMessageHandler>();
        return serviceCollection;
    }
}