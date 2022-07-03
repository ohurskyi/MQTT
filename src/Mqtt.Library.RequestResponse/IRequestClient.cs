﻿using MessagingLibrary.Core.Messages;
using Mqtt.Library.Client.Configuration;

namespace Mqtt.Library.RequestResponse;

public interface IRequestClient<TMessagingClientOptions>
    where TMessagingClientOptions : IMqttMessagingClientOptions
{
    Task<TMessageResponse> SendAndWaitAsync<TMessageResponse>(string requestTopic, string responseTopic, IMessageContract contract, TimeSpan timeout) where TMessageResponse : class, IMessageResponse;
}