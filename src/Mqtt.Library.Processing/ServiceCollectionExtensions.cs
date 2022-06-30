using System.Reflection;
using MessagingLibrary.Processing;
using MessagingLibrary.Processing.Middlewares;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Mqtt.Library.Client.Configuration;
using Mqtt.Library.Processing.Middlewares;

namespace Mqtt.Library.Processing;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMqttMessagingPipeline<TMessagingClientOptions>(this IServiceCollection serviceCollection, params Assembly[] assemblies)
        where TMessagingClientOptions : class, IMqttMessagingClientOptions
    {
        return serviceCollection
            .AddMessagingPipeline<TMessagingClientOptions>(assemblies)
            .AddMqttTopicComparer()
            .AddInternalMiddleware()
            .AddMqttApplicationMessageReceivedHandler<TMessagingClientOptions>();
    }

    private static IServiceCollection AddMqttApplicationMessageReceivedHandler<TMessagingClientOptions>(this IServiceCollection serviceCollection)
        where TMessagingClientOptions : class, IMqttMessagingClientOptions
    {
        serviceCollection.TryAddSingleton<MqttReceivedMessageHandler<TMessagingClientOptions>>();
        return serviceCollection;
    }
    
    private static IServiceCollection AddInternalMiddleware(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMiddleware<LoggingMiddleware>();
        serviceCollection.AddMiddleware<PublishMiddleware>();
        serviceCollection.AddMiddleware<ReplyMiddleware>();
        return serviceCollection;
    }
}