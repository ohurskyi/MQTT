﻿using Mqtt.Library.Core.Configuration;
using Mqtt.Library.Core.Messages;

namespace Mqtt.Library.Core.Strategy;

public interface IMessageHandlingStrategy<TMessagingClientOptions> where TMessagingClientOptions: IMessagingClientOptions
{
    Task Handle(IMessage message);
}