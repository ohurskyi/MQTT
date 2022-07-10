﻿using MessagingLibrary.Client.Mqtt.Configuration;
using MessagingLibrary.Core.Handlers;
using MessagingLibrary.TopicClient.Mqtt.Definitions;

namespace MessagingLibrary.TopicClient.Mqtt
{
    public interface IMqttTopicClient<TMessagingClientOptions> where TMessagingClientOptions : IMqttMessagingClientOptions
    {
        Task<ISubscription> Subscribe<T>(string topic) where T : class, IMessageHandler;
        Task Subscribe(IDefinition definition);
        Task Unsubscribe(ISubscription subscription);
    }
}