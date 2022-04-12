﻿using MessagingClient.Mqtt;
using MessagingLibrary.Mqtt.Local;
using Mqtt.Library.Test.Client;
using Mqtt.Library.Test.Core;
using Mqtt.Library.Test.Handlers;
using MqttClientTest.Messaging.Processing;
using MQTTnet;

namespace Mqtt.Library.Test
{
    public class BackgroundMqttPublisher : BackgroundService
    {
        private readonly ITopicClient<LocalMqttMessagingClientOptions> _topicClient;

        public BackgroundMqttPublisher(ITopicClient<LocalMqttMessagingClientOptions> topicClient)
        {
            _topicClient = topicClient;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            var topic = "some-topic";
            await _topicClient.Subscribe<MessageHandlerTest>(topic);
            await _topicClient.Subscribe<MessageHandlerTest2>(topic);
            
            await base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(3), stoppingToken);
            
            while (!stoppingToken.IsCancellationRequested)
            {
                var topic = "some-topic";
                var mqttMessage = new MqttApplicationMessageBuilder().WithTopic(topic);
                await _topicClient.Publish(mqttMessage.Build());
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }
    }
}