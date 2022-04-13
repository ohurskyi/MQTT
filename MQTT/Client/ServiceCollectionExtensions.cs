using Microsoft.Extensions.DependencyInjection.Extensions;
using Mqtt.Library.Client.Configuration;
using Mqtt.Library.Client.Services;
using Mqtt.Library.Test.Client;
using Mqtt.Library.Test.Core;

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