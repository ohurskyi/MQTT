using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Mqtt.Library.MessageBus;
using Mqtt.Library.TopicClient;

namespace Mqtt.Library.Client.Local;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddLocalMqttMessagingClient(this IServiceCollection serviceCollection, IConfiguration configuration)
    {
        return serviceCollection.AddMqttMessagingClient<LocalMqttMessagingClientOptions>(configuration);
    }
    
    public static IServiceCollection AddLocalMqttMessageBus(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddMqttMessageBus<LocalMqttMessagingClientOptions>();
    }

    public static IServiceCollection AddLocalMqttTopicClient(this IServiceCollection serviceCollection)
    {
        return serviceCollection.AddMqttTopicClient<LocalMqttMessagingClientOptions>();
    }
}