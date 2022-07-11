﻿using MessagingLibrary.Client.Mqtt.Configuration;

namespace MessagingLibrary.TopicClient.Mqtt.Definitions;

public interface IConsumerDefinitionProvider<TMessagingClientOptions>
    where TMessagingClientOptions : class, IMqttMessagingClientOptions
{
    IEnumerable<IConsumerDefinition> ConsumerDefinitions();
}