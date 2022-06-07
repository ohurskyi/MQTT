﻿using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Mqtt.Library.Core.Configuration;
using Mqtt.Library.Core.Factory;
using Mqtt.Library.Core.Processing;
using Mqtt.Library.Core.Strategy;

namespace Mqtt.Library.Core;

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
        serviceCollection.TryAddSingleton<IMessageHandlerFactory<TMessagingClientOptions>, MessageHandlerFactory<TMessagingClientOptions>>();

        serviceCollection.TryAddTransient<HandlerFactory>(p => p.GetRequiredService);

        serviceCollection.TryAddTransient<IMessageHandlingStrategy<TMessagingClientOptions>, MessageHandlingStrategy<TMessagingClientOptions>>();
        
        serviceCollection.TryAddSingleton<IMessageExecutor<TMessagingClientOptions>, ScopedMessageExecutor<TMessagingClientOptions>>();

        return serviceCollection;
    }
}