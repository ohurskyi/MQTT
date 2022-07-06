using MessagingLibrary.Client.Mqtt.Configuration;
using MessagingLibrary.Core;
using MessagingLibrary.Core.Configuration.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Mqtt.Library.RequestResponse.Completion;
using Mqtt.Library.RequestResponse.Handlers;
using Mqtt.Library.RequestResponse.Requesters;

namespace Mqtt.Library.RequestResponse;

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