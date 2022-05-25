using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Mqtt.Library.Client.Configuration;
using Mqtt.Library.Core;
using Mqtt.Library.Core.Factory;

namespace Mqtt.Library.Processing;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMqttMessagingPipeline<TMessagingClientOptions>(this IServiceCollection serviceCollection, params Assembly[] assemblies)
        where TMessagingClientOptions : class, IMqttMessagingClientOptions
    {
        return serviceCollection
            .AddMessagingPipeline<TMessagingClientOptions>(assemblies)
            .AddMqttTopicComparer()
            .AddMqttApplicationMessageReceivedHandler<TMessagingClientOptions>();
    }

    public static IServiceCollection AddMqttStartupListener<TStartupListener>(this IServiceCollection serviceCollection)
        where TStartupListener: class, IHostedService
    {
        serviceCollection.AddHostedService<TStartupListener>();
        return serviceCollection;
    }
    
    private static IServiceCollection AddMqttApplicationMessageReceivedHandler<TMessagingClientOptions>(this IServiceCollection serviceCollection)
        where TMessagingClientOptions : class, IMqttMessagingClientOptions
    {
        serviceCollection.TryAddSingleton<MqttReceivedMessageHandler<TMessagingClientOptions>>();
        return serviceCollection;
    }

    private static IServiceCollection AddMqttTopicComparer(this IServiceCollection serviceCollection)
    { 
        serviceCollection.TryAddSingleton<ITopicFilterComparer, MqttTopicComparer>();
        return serviceCollection;
    }
}