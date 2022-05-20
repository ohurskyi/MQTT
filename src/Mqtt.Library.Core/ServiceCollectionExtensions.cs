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
        var implementationTypes = assemblies
            .SelectMany(a => a.GetTypes())
            .Where(t => typeof(IMessageHandler).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .ToList();

        foreach (var handlerType in implementationTypes)
        {
            serviceCollection.AddTransient(handlerType);
        }

        serviceCollection.AddSingleton<IMessageHandlerFactory<T>, MessageHandlerFactory<T>>();
        
        serviceCollection.TryAddTransient<HandlerFactory>(p => p.GetRequiredService);
        
        serviceCollection.AddTransient<IMessageHandlingStrategy<T>, MessageHandlingStrategy<T>>();
        serviceCollection.AddMessageExecutor<T>();
        serviceCollection.AddMiddlewareTest();

        return serviceCollection;
    }
    
    private static IServiceCollection AddMessageExecutor<T>(this IServiceCollection serviceCollection)
        where T: class
    {
        return serviceCollection.AddSingleton<IMessageExecutor<T>, ScopedMessageExecutor<T>>();
    }

    private static IServiceCollection AddMiddlewareTest(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddTransient<IMessageMiddleware, LoggingMiddleware>();
    }
}