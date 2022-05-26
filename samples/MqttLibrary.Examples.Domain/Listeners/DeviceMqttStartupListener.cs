﻿using Microsoft.Extensions.Logging;
using Mqtt.Library.Client.Local;
using Mqtt.Library.Processing.Listeners;
using Mqtt.Library.TopicClient;
using MqttLibrary.Examples.Contracts.Topics;
using MqttLibrary.Examples.Domain.Handlers;

namespace MqttLibrary.Examples.Domain.Listeners;

public class DeviceMqttStartupListener : BaseMqttStartupListener<LocalMqttMessagingClientOptions>
{

    protected override IEnumerable<Task<ISubscription>> DefineSubscriptions()
    {
        var subscriptions = new List<Task<ISubscription>>
        {
            TopicClient.Subscribe<HandlerForDeviceNumber1>($"{TopicConstants.DeviceTopic}/{1}"),
            TopicClient.Subscribe<HandlerForDeviceNumber2>($"{TopicConstants.DeviceTopic}/{2}"),
            TopicClient.Subscribe<HandlerForAllDeviceNumbers>($"{TopicConstants.DeviceTopic}/#"),
        };
        return subscriptions;
    }

    public DeviceMqttStartupListener(
            IMqttTopicClient<LocalMqttMessagingClientOptions> topicClient, 
            ILogger<BaseMqttStartupListener<LocalMqttMessagingClientOptions>> logger) : base(topicClient, logger)
    {
    }
}