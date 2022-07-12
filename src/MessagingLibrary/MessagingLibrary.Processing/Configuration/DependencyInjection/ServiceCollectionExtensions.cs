using System.Reflection;
using MessagingLibrary.Core.Configuration;
using MessagingLibrary.Core.Configuration.DependencyInjection;
using MessagingLibrary.Processing.Executor;
using MessagingLibrary.Processing.Listeners;
using MessagingLibrary.Processing.Strategy;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MessagingLibrary.Processing.Configuration.DependencyInjection;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMessagingPipeline<TMessagingClientOptions>(this IServiceCollection serviceCollection, params Assembly[] assemblies)
        where TMessagingClientOptions: IMessagingClientOptions
    {
        assemblies = assemblies.Distinct().ToArray();
        
        serviceCollection.AddMessageHandlers(assemblies);

        serviceCollection.AddRequiredServices<TMessagingClientOptions>();

        return serviceCollection;
    }

    private static IServiceCollection AddRequiredServices<TMessagingClientOptions>(this IServiceCollection serviceCollection) 
        where TMessagingClientOptions: IMessagingClientOptions
    {
        serviceCollection.AddMessageHandlerFactory<TMessagingClientOptions>();

        serviceCollection.TryAddTransient<IMessageHandlingStrategy<TMessagingClientOptions>, MessageHandlingStrategy<TMessagingClientOptions>>();
        
        serviceCollection.TryAddSingleton<IMessageExecutor<TMessagingClientOptions>, ScopedMessageExecutor<TMessagingClientOptions>>();

        return serviceCollection;
    }
    
    public static IServiceCollection AddConsumerDefinitionProvider<TConsumerDefinitionProvider>(this IServiceCollection serviceCollection)
        where TConsumerDefinitionProvider: class, IConsumerDefinitionProvider
    {
        serviceCollection.TryAddSingleton<IConsumerDefinitionProvider, TConsumerDefinitionProvider>();
        return serviceCollection;
    }
    
    public static IServiceCollection AddConsumerDefinitionListenerProvider<TConsumerDefinitionListenerProvider>(this IServiceCollection serviceCollection)
        where TConsumerDefinitionListenerProvider: class, IConsumerDefinitionListenerProvider
    {
        serviceCollection.TryAddSingleton<IConsumerDefinitionListenerProvider, TConsumerDefinitionListenerProvider>();
        return serviceCollection;
    }

    public static IServiceCollection AddConsumerListener(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddHostedService<MessageConsumersHostedService>();
        return serviceCollection;
    }
}