using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Mqtt.Library.Client.Configuration;

namespace Mqtt.Library.RequestResponse;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMqttRequestClient<TMessagingClientOptions>(this IServiceCollection serviceCollection) 
        where TMessagingClientOptions: class, IMqttMessagingClientOptions
    {
        serviceCollection.TryAddSingleton<PendingResponseTracker>();
        serviceCollection.TryAddSingleton<IRequester<TMessagingClientOptions>, Requester<TMessagingClientOptions>>();
        serviceCollection.TryAddSingleton<IRequestClient<TMessagingClientOptions>, RequestClient<TMessagingClientOptions>>();
        return serviceCollection;
    }
}