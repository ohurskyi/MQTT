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
        private readonly IMqttMessageBus<LocalMqttMessagingClientOptions> _mqttMessageBus;

        public BackgroundMqttPublisher(ITopicClient<LocalMqttMessagingClientOptions> topicClient, IMqttMessageBus<LocalMqttMessagingClientOptions> mqttMessageBus)
        {
            _topicClient = topicClient;
            _mqttMessageBus = mqttMessageBus;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            var subTopic = "device/#";
            await _topicClient.Subscribe<MessageHandlerTest>(subTopic);
            await _topicClient.Subscribe<MessageHandlerTest2>(subTopic);
            
            await base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(3), stoppingToken);
            
            while (!stoppingToken.IsCancellationRequested)
            {
                var pubTopic = "device/1";
                var mqttMessage = new MqttApplicationMessageBuilder().WithTopic(pubTopic);
                await _mqttMessageBus.Publish(mqttMessage.Build());
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }
    }
}