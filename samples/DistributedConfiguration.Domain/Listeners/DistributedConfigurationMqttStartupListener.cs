﻿using DistributedConfiguration.Contracts.Topics;
using DistributedConfiguration.Domain.Handlers;
using MessagingLibrary.Processing.Mqtt.Listeners;
using MessagingLibrary.TopicClient.Mqtt;
using Microsoft.Extensions.Logging;
using Mqtt.Library.Client.Infrastructure;

namespace DistributedConfiguration.Domain.Listeners;

public class DistributedConfigurationMqttStartupListener : BaseMqttStartupListener<InfrastructureMqttMessagingClientOptions>
{
    protected override IEnumerable<Task<ISubscription>> DefineSubscriptions()
    {
        var subscriptions = new List<Task<ISubscription>>
        {
            TopicClient.Subscribe<PairDeviceMessageHandler>($"{TopicConstants.PairDevice}"),
            TopicClient.Subscribe<GetPairedDeviceMessageHandler>($"{TopicConstants.RequestUpdate}"),
        };
        return subscriptions;
    }

    public DistributedConfigurationMqttStartupListener(
            IMqttTopicClient<InfrastructureMqttMessagingClientOptions> topicClient, 
            ILogger<BaseMqttStartupListener<InfrastructureMqttMessagingClientOptions>> logger) : base(topicClient, logger)
    {
    }
}