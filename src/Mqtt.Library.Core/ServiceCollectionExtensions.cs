using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Mqtt.Library.Core.Factory;

namespace Mqtt.Library.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMessagingPipeline(this IServiceCollection serviceCollection, params Assembly[] assemblies)
    {
        var implementationTypes = assemblies
            .SelectMany(a => a.GetTypes())
            .Where(t => typeof(IMessageHandler).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .ToList();

        foreach (var handlerType in implementationTypes)
        {
            serviceCollection.AddTransient(handlerType);
        }

        serviceCollection.AddSingleton<IMessageHandlerFactory, MessageHandlerFactory>();
        serviceCollection.AddTransient<HandlerFactory>(p => p.GetRequiredService);
        serviceCollection.AddTransient<IMessageHandlingStrategy, MessageHandlingStrategy>();

        return serviceCollection;
    }
}