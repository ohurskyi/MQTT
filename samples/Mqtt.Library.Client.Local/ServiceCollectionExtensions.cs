using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mqtt.Library.MessageBus;
using Mqtt.Library.TopicClient;

namespace Mqtt.Library.Client.Local;

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