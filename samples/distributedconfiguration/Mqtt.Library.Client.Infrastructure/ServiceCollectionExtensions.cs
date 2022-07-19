using MessagingLibrary.Processing.Mqtt.Configuration.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Mqtt.Library.Client.Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructureMqttMessagingClient(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        return serviceCollection.AddMqttMessagingClient<InfrastructureMqttMessagingClientOptions>(configuration);
    }
    
    public static IServiceCollection AddInfrastructureMqttMessageBus(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddMqttMessageBus<InfrastructureMqttMessagingClientOptions>();
    }

    public static IServiceCollection AddInfrastructureMqttTopicClient(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddMqttTopicClient<InfrastructureMqttMessagingClientOptions>();
    }
}