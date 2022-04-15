using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Mqtt.Library.Core;
using Mqtt.Library.Processing.Executor;
using Mqtt.Library.Processing.Factory;

namespace Mqtt.Library.Processing;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMessageProcessing(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<IMqttMessageExecutor, ScopedMessageExecutor>();
        serviceCollection.AddSingleton<IMessageHandlerFactory, MessageHandlerFactory>();
        
        serviceCollection.AddScoped<IMessageHandlingStrategy, MessageHandlingStrategy>();
        
        return serviceCollection;
    }
    
    public static IServiceCollection AddMqttApplicationMessageReceivedHandler(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<MqttReceivedMessageHandler>();
        return serviceCollection;
    }

    public static IServiceCollection AddMqttMessagingPipeline(this IServiceCollection serviceCollection, params Assembly[] assemblies)
    {
        var implementationTypes = assemblies
            .SelectMany(a => a.GetTypes())
            .Where(t => typeof(IMessageHandler).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract)
            .ToList();

        foreach (var handlerType in implementationTypes)
        {
            serviceCollection.AddTransient(handlerType);
        }
            
        serviceCollection.AddMessageProcessing();
        serviceCollection.AddMqttApplicationMessageReceivedHandler();
        return serviceCollection;
    }
}