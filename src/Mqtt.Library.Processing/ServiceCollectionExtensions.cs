using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Mqtt.Library.Client.Configuration;
using Mqtt.Library.Core;
using Mqtt.Library.Processing.Listeners;

namespace Mqtt.Library.Processing;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMqttMessagingPipeline(this IServiceCollection serviceCollection, params Assembly[] assemblies)
    {
        return serviceCollection
            .AddMessagingPipeline(assemblies)
            .AddMqttApplicationMessageReceivedHandler();
    }

    public static IServiceCollection AddMqttStartupListener<TMessagingClientOptions, TStartupListener>(this IServiceCollection serviceCollection)
        where TMessagingClientOptions : class, IMqttMessagingClientOptions, new()
        where TStartupListener: class, IMqttStartupListener<TMessagingClientOptions>
    {
        serviceCollection.AddSingleton<IMqttStartupListener<TMessagingClientOptions>, TStartupListener>();
        serviceCollection.AddHostedService<MqttMessageListener<TMessagingClientOptions>>();
        return serviceCollection;
    }
    
    private static IServiceCollection AddMqttApplicationMessageReceivedHandler(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<MqttReceivedMessageHandlerGen>();
        return serviceCollection;
    }
}