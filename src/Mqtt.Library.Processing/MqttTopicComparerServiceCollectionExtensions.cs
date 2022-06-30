using MessagingLibrary.Core.Factory;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Mqtt.Library.Processing;

public static class MqttTopicComparerServiceCollectionExtensions
{
    public static IServiceCollection AddMqttTopicComparer(this IServiceCollection serviceCollection)
    { 
        serviceCollection.TryAddSingleton<ITopicFilterComparer, MqttTopicComparer>();
        return serviceCollection;
    }
}