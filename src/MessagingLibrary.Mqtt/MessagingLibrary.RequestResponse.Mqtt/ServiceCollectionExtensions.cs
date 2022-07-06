using MessagingLibrary.Client.Mqtt.Configuration;
using MessagingLibrary.Core.Configuration.DependencyInjection;
using MessagingLibrary.RequestResponse.Mqtt.Completion;
using MessagingLibrary.RequestResponse.Mqtt.Handlers;
using MessagingLibrary.RequestResponse.Mqtt.Requesters;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MessagingLibrary.RequestResponse.Mqtt;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMqttRequestClient<TMessagingClientOptions>(this IServiceCollection serviceCollection) 
        where TMessagingClientOptions: class, IMqttMessagingClientOptions
    {
        serviceCollection.TryAddSingleton<PendingResponseTracker>();
        serviceCollection.AddMessageHandler<ResponseHandler>();
        serviceCollection.TryAddSingleton<IRequester<TMessagingClientOptions>, Requester<TMessagingClientOptions>>();
        serviceCollection.TryAddSingleton<IRequestClient<TMessagingClientOptions>, RequestClient<TMessagingClientOptions>>();
        return serviceCollection;
    }
}