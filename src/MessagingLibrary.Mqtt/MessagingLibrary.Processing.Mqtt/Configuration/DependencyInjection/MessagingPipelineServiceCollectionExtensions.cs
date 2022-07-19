using System.Reflection;
using MessagingLibrary.Processing.Configuration.DependencyInjection;
using MessagingLibrary.Processing.Mqtt.Configuration.Configuration;
using MessagingLibrary.Processing.Mqtt.Middlewares;
using Microsoft.Extensions.DependencyInjection;

namespace MessagingLibrary.Processing.Mqtt.Configuration.DependencyInjection;

public static class MessagingPipelineServiceCollectionExtensions
{
    public static IServiceCollection AddMqttMessagingPipeline<TMessagingClientOptions>(this IServiceCollection serviceCollection)
        where TMessagingClientOptions : class, IMqttMessagingClientOptions
    {
        return serviceCollection
            .AddMessagingPipeline<TMessagingClientOptions>()
            .AddMqttTopicComparer()
            .AddInternalMiddlewares()
            .AddMqttApplicationMessageReceivedHandler<TMessagingClientOptions>();
    }

    private static IServiceCollection AddInternalMiddlewares(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddMiddleware<UnhandledExceptionMiddleware>();
        serviceCollection.AddMiddleware<LoggingMiddleware>();
        serviceCollection.AddMiddleware<PublishMiddleware>();
        serviceCollection.AddMiddleware<ReplyMiddleware>();
        return serviceCollection;
    }
}