using MessagingLibrary.Core.Factory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MessagingLibrary.Processing.Mqtt.Configuration.DependencyInjection;

public static class TopicComparerServiceCollectionExtensions
{
    public static IServiceCollection AddMqttTopicComparer(this IServiceCollection serviceCollection)
    { 
        serviceCollection.TryAddSingleton<ITopicFilterComparer, MqttTopicComparer>();
        return serviceCollection;
    }
}