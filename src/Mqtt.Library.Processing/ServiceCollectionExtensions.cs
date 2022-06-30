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
            .AddMiddleware()
            .AddMqttApplicationMessageReceivedHandler<TMessagingClientOptions>();
    }

    private static IServiceCollection AddMqttApplicationMessageReceivedHandler<TMessagingClientOptions>(this IServiceCollection serviceCollection)
        where TMessagingClientOptions : class, IMqttMessagingClientOptions
    {
        serviceCollection.TryAddSingleton<MqttReceivedMessageHandler<TMessagingClientOptions>>();
        return serviceCollection;
    }

    private static IServiceCollection AddMiddleware(this IServiceCollection serviceCollection)
    {
        serviceCollection.TryAddEnumerable(new[]
        {
            ServiceDescriptor.Transient(typeof(IMessageMiddleware), typeof(LoggingMiddleware)),
            ServiceDescriptor.Transient(typeof(IMessageMiddleware), typeof(PublishMiddleware)),
            ServiceDescriptor.Transient(typeof(IMessageMiddleware), typeof(ReplyMiddleware))
        });

        return serviceCollection;
    }
}