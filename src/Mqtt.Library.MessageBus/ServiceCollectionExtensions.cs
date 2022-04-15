﻿using Microsoft.Extensions.DependencyInjection;
using Mqtt.Library.Client.Configuration;

namespace Mqtt.Library.MessageBus;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddMqttMessageBus<TMessagingClientOptions>(this IServiceCollection serviceCollection) where TMessagingClientOptions: IMqttMessagingClientOptions
    {
        serviceCollection.AddSingleton<IMqttMessageBus<TMessagingClientOptions>, MqttMqttMessageBus<TMessagingClientOptions>>();
        return serviceCollection;
    }
}