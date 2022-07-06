using System.Reflection;
using MessagingLibrary.Client.Mqtt.Configuration;
using MessagingLibrary.Processing.Configuration.DependencyInjection;
using MessagingLibrary.Processing.Mqtt.Middlewares;
using Microsoft.Extensions.DependencyInjection;

namespace MessagingLibrary.Processing.Mqtt.Configuration.DependencyInjection;

public static class MessagingPipelineServiceCollectionExtensions
{
    public static IServiceCollection AddMqttMessagingPipeline<TMessagingClientOptions>(this IServiceCollection serviceCollection, params Assembly[] assemblies)
        where TMessagingClientOptions : class, IMqttMessagingClientOptions
    {
        return serviceCollection
            .AddMessagingPipeline<TMessagingClientOptions>(assemblies)
            .AddMqttTopicComparer()
            .AddInternalMiddlewares()
            .AddMqttApplicationMessageReceivedHandler<TMessagingClientOptions>();
    }

    private static IServiceCollection AddInternalMiddlewares(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMiddleware<LoggingMiddleware>();
        serviceCollection.AddMiddleware<PublishMiddleware>();
        serviceCollection.AddMiddleware<ReplyMiddleware>();
        return serviceCollection;
    }
}