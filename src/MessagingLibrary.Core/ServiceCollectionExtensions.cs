using System.Reflection;
using MessagingLibrary.Core.Configuration;
using MessagingLibrary.Core.Factory;
using MessagingLibrary.Core.Handlers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MessagingLibrary.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMessageHandlerFactory<TMessagingClientOptions>(this IServiceCollection serviceCollection) 
        where TMessagingClientOptions: IMessagingClientOptions
    {
        serviceCollection.TryAddTransient<HandlerFactory>(p => p.GetRequiredService);
        serviceCollection.TryAddSingleton<IMessageHandlerFactory<TMessagingClientOptions>, MessageHandlerFactory<TMessagingClientOptions>>();
        return serviceCollection;
    }

    public static IServiceCollection AddMessageHandler<T>(this IServiceCollection serviceCollection) where T : class, IMessageHandler
    { 
        serviceCollection.TryAddTransient<T>();
        return serviceCollection;
    }
    
    public static IServiceCollection AddMessageHandlers(this IServiceCollection serviceCollection, Assembly[] assemblies)
    {
        var implementationTypes = assemblies
            .SelectMany(a => a.DefinedTypes)
            .Where(t => typeof(IMessageHandler).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .ToList();

        foreach (var handlerType in implementationTypes)
        {
            serviceCollection.AddTransient(handlerType);
        }

        return serviceCollection;
    }
}