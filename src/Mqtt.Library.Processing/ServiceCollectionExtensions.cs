using System.Reflection;
using Microsoft.Extensions.DependencyInjection;
using Mqtt.Library.Core;
using Mqtt.Library.Processing.Executor;

namespace Mqtt.Library.Processing;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMqttMessagingPipelineGen(this IServiceCollection serviceCollection, params Assembly[] assemblies)
    {
        return serviceCollection
            .AddMessagingPipelineGen(assemblies)
            .AddMessageExecutor()
            .AddMqttApplicationMessageReceivedHandlerGen();
    }
    
    private static IServiceCollection AddMqttApplicationMessageReceivedHandlerGen(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<MqttReceivedMessageHandlerGen>();
        return serviceCollection;
    }

    private static IServiceCollection AddMessageExecutor(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddSingleton<IMqttMessageExecutor, ScopedMessageExecutorGen>();
    }
}