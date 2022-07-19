using MessagingLibrary.Core.Clients;
using MessagingLibrary.Core.Configuration.DependencyInjection;
using MessagingLibrary.Processing.Mqtt.Clients;
using MessagingLibrary.Processing.Mqtt.Clients.RequestResponse;
using MessagingLibrary.Processing.Mqtt.Clients.RequestResponse.Completion;
using MessagingLibrary.Processing.Mqtt.Clients.RequestResponse.Handlers;
using MessagingLibrary.Processing.Mqtt.Clients.RequestResponse.Requesters;
using MessagingLibrary.Processing.Mqtt.Configuration.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MessagingLibrary.Processing.Mqtt.Configuration.DependencyInjection;

public static class MessageClientServiceCollectionExtensions
{
    public static IServiceCollection AddMqttMessageBus<TMessagingClientOptions>(this IServiceCollection serviceCollection) where TMessagingClientOptions: IMqttMessagingClientOptions
    {
        serviceCollection.TryAddSingleton<IMessageBus<TMessagingClientOptions>, MqttMessageBus<TMessagingClientOptions>>();
        return serviceCollection;
    }
    
    public static IServiceCollection AddMqttTopicClient<TMessagingClientOptions>(this IServiceCollection serviceCollection) 
        where TMessagingClientOptions: class, IMqttMessagingClientOptions
    {
        serviceCollection.TryAddSingleton<ITopicClient<TMessagingClientOptions>, MqttTopicClient<TMessagingClientOptions>>();
        return serviceCollection;
    }
    
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