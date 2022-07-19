using MessagingLibrary.Core.Clients;
using MessagingLibrary.Processing.Mqtt.Clients;
using MessagingLibrary.Processing.Mqtt.Configuration.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MessagingLibrary.Processing.Mqtt.Configuration.DependencyInjection;

public static class MqttTopicClientServiceCollectionExtensions
{
    public static IServiceCollection AddMqttTopicClient<TMessagingClientOptions>(this IServiceCollection serviceCollection) 
        where TMessagingClientOptions: class, IMqttMessagingClientOptions
    {
        serviceCollection.TryAddSingleton<ITopicClient<TMessagingClientOptions>, MqttTopicClient<TMessagingClientOptions>>();
        return serviceCollection;
    }
}