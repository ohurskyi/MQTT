using MessagingLibrary.Client.Mqtt.Configuration;
using MessagingLibrary.Core.Messages;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace MessagingLibrary.MessageBus.Mqtt;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMqttMessageBus<TMessagingClientOptions>(this IServiceCollection serviceCollection) where TMessagingClientOptions: IMqttMessagingClientOptions
    {
        serviceCollection.TryAddSingleton<IMessageBus<TMessagingClientOptions>, MqttMessageBus<TMessagingClientOptions>>();
        return serviceCollection;
    }
}