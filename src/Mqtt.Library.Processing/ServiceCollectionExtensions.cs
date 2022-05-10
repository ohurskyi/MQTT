using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Mqtt.Library.Core;
using Mqtt.Library.Core.Processing;

namespace Mqtt.Library.Processing;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMqttMessagingPipeline(this IServiceCollection serviceCollection, params Assembly[] assemblies)
    {
        return serviceCollection
            .AddMessagingPipeline(assemblies)
            .AddMqttApplicationMessageReceivedHandler();
    }
    
    private static IServiceCollection AddMqttApplicationMessageReceivedHandler(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<MqttReceivedMessageHandlerGen>();
        return serviceCollection;
    }
}