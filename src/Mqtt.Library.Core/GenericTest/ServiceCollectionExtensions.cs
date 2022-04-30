using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace Mqtt.Library.Core.GenericTest;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMessagingPipelineGen(this IServiceCollection serviceCollection, params Assembly[] assemblies)
    {
        var implementationTypes = assemblies
            .SelectMany(a => a.GetTypes())
            .Where(t => typeof(IMessageHandlerGen).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .ToList();

        foreach (var handlerType in implementationTypes)
        {
            serviceCollection.AddTransient(handlerType);
        }

        serviceCollection.AddSingleton<IMessageHandlerFactoryGen, MessageHandlerFactoryGen>();
        serviceCollection.AddScoped<MessageHandlingStrategyGen>();
        
        return serviceCollection;
    }
}