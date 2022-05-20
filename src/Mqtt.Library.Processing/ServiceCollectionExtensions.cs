using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Mqtt.Library.Client.Configuration;
using Mqtt.Library.Core;
using Mqtt.Library.Processing.Listeners;

namespace Mqtt.Library.Processing;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMqttMessagingPipeline<TMessagingClientOptions>(this IServiceCollection serviceCollection, params Assembly[] assemblies)
        where TMessagingClientOptions : class, IMqttMessagingClientOptions
    {
        return serviceCollection
            .AddMessagingPipeline<TMessagingClientOptions>(assemblies)
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
        serviceCollection.AddSingleton<MqttReceivedMessageHandler<TMessagingClientOptions>>();
        return serviceCollection;
    }
}