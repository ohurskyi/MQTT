using MessagingLibrary.Processing.Mqtt.Configuration.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mqtt.Library.Client.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureMqttMessagingClient(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        return serviceCollection.AddMqttMessagingClient<InfrastructureClientOptions>(configuration);
    }
    
    public static IServiceCollection AddInfrastructureMqttMessageBus(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddMqttMessageBus<InfrastructureClientOptions>();
    }

    public static IServiceCollection AddInfrastructureMqttTopicClient(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddMqttTopicClient<InfrastructureClientOptions>();
    }

    public static IServiceCollection AddInfrastructureMqttRequestClient(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddMqttRequestClient<InfrastructureClientOptions>();
    }

    public static IServiceCollection AddInfrastructureMqttPipe(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddMqttPipe<InfrastructureClientOptions>();
    }
}