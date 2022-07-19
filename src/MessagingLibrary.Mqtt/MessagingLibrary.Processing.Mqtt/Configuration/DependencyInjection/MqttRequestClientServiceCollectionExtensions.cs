using MessagingLibrary.Core.Configuration.DependencyInjection;
using MessagingLibrary.Processing.Mqtt.Clients.RequestResponse;
using MessagingLibrary.Processing.Mqtt.Clients.RequestResponse.Completion;
using MessagingLibrary.Processing.Mqtt.Clients.RequestResponse.Handlers;
using MessagingLibrary.Processing.Mqtt.Clients.RequestResponse.Requesters;
using MessagingLibrary.Processing.Mqtt.Configuration.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MessagingLibrary.RequestResponse.Mqtt;

public static class MqttRequestClientServiceCollectionExtensions
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