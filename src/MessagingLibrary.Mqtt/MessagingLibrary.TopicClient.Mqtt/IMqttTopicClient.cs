﻿using MessagingLibrary.Client.Mqtt.Configuration;
using MessagingLibrary.Core.Handlers;

namespace MessagingLibrary.TopicClient.Mqtt
{
    public interface IMqttTopicClient<TMessagingClientOptions> where TMessagingClientOptions : IMqttMessagingClientOptions
    {
        Task<ISubscription> Subscribe<T>(string topic) where T : class, IMessageHandler;
        Task Unsubscribe(ISubscription subscription);
    }
}