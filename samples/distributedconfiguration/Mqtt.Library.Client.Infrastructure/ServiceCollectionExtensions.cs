using MessagingLibrary.Processing.Mqtt.Configuration.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mqtt.Library.Client.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureMessagingClient(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        return serviceCollection.AddMqttMessagingClient<InfrastructureClientOptions, InfrastructureClientOptionsBuilder>(configuration);
    }
    
    public static IServiceCollection AddInfrastructureMessageBus(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddMqttMessageBus<InfrastructureClientOptions>();
    }

    public static IServiceCollection AddInfrastructureTopicClient(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddMqttTopicClient<InfrastructureClientOptions>();
    }

    public static IServiceCollection AddInfrastructureRequestClient(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddMqttRequestClient<InfrastructureClientOptions>();
    }

    public static IServiceCollection AddInfrastructureMqttPipe(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddMqttPipe<InfrastructureClientOptions>();
    }
}