using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Mqtt.Library.Core.Factory;
using Mqtt.Library.Core.Middleware;
using Mqtt.Library.Core.Processing;
using Mqtt.Library.Core.Strategy;

namespace Mqtt.Library.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMessagingPipeline<T>(this IServiceCollection serviceCollection, params Assembly[] assemblies)
        where T:class
    {
        ConnectMessageHandlers(serviceCollection, assemblies);

        serviceCollection.AddRequiredServices<T>();

        serviceCollection.AddMiddlewareTest();

        return serviceCollection;
    }

    private static void ConnectMessageHandlers(IServiceCollection serviceCollection, Assembly[] assemblies)
    {
        var implementationTypes = assemblies
            .SelectMany(a => a.DefinedTypes)
            .Where(t => typeof(IMessageHandler).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .ToList();

        foreach (var handlerType in implementationTypes)
        {
            serviceCollection.AddTransient(handlerType);
        }
    }

    private static IServiceCollection AddRequiredServices<T>(this IServiceCollection serviceCollection) where T : class
    {
        serviceCollection.TryAddSingleton<IMessageHandlerFactory<T>, MessageHandlerFactory<T>>();

        serviceCollection.TryAddTransient<HandlerFactory>(p => p.GetRequiredService);

        serviceCollection.TryAddTransient<IMessageHandlingStrategy<T>, MessageHandlingStrategy<T>>();
        
        serviceCollection.TryAddSingleton<IMessageExecutor<T>, ScopedMessageExecutor<T>>();

        return serviceCollection;
    }

    private static IServiceCollection AddMiddlewareTest(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddTransient<IMessageMiddleware, LoggingMiddleware>();
    }
}