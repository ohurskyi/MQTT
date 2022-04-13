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
        
        serviceCollection.AddScoped<MessageHandlerWrapper>();
        
        return serviceCollection;
    }
    
    public static IServiceCollection AddMqttApplicationMessageReceivedHandler(this IServiceCollection serviceCollection)
    {
        serviceCollection.AddSingleton<MqttReceivedMessageHandler>();
        return serviceCollection;
    }
}