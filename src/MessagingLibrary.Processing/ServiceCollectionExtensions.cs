using System.Reflection;
using MessagingLibrary.Core;
using MessagingLibrary.Core.Configuration;
using MessagingLibrary.Core.Factory;
using MessagingLibrary.Core.Handlers;
using MessagingLibrary.Processing.Executor;
using MessagingLibrary.Processing.Strategy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MessagingLibrary.Processing;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMessagingPipeline<TMessagingClientOptions>(this IServiceCollection serviceCollection, params Assembly[] assemblies)
        where TMessagingClientOptions: IMessagingClientOptions
    {
        ConnectMessageHandlers(serviceCollection, assemblies);

        serviceCollection.AddRequiredServices<TMessagingClientOptions>();

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

    private static IServiceCollection AddRequiredServices<TMessagingClientOptions>(this IServiceCollection serviceCollection) 
        where TMessagingClientOptions: IMessagingClientOptions
    {
        serviceCollection.AddMessageHandlerFactory<TMessagingClientOptions>();

        serviceCollection.TryAddTransient<IMessageHandlingStrategy<TMessagingClientOptions>, MessageHandlingStrategy<TMessagingClientOptions>>();
        
        serviceCollection.TryAddSingleton<IMessageExecutor<TMessagingClientOptions>, ScopedMessageExecutor<TMessagingClientOptions>>();

        return serviceCollection;
    }
}