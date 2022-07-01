using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Mqtt.Library.Client.Configuration;

namespace MessagingLibrary.Processing.Mqtt.Configuration.DependencyInjection;

public static class ApplicationMessageHandlerServiceCollectionExtensions
{
    public static IServiceCollection AddMqttApplicationMessageReceivedHandler<TMessagingClientOptions>(this IServiceCollection serviceCollection) 
        where TMessagingClientOptions : class, IMqttMessagingClientOptions
    {
        serviceCollection.TryAddSingleton<MqttReceivedMessageHandler<TMessagingClientOptions>>();
        return serviceCollection;
    }
}