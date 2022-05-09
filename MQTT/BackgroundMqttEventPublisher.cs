﻿using Mqtt.Library.Core;
using Mqtt.Library.MessageBus;
using Mqtt.Library.Test.ClientOptions;
using Mqtt.Library.Test.Handlers;
using Mqtt.Library.Test.Payloads;
using Mqtt.Library.TopicClient;

namespace Mqtt.Library.Test
{
    public class BackgroundLocalMqttPublisher : BackgroundService
    {
        private readonly IMqttTopicClient<LocalMqttMessagingClientOptions> _topicClient;
        private readonly IMqttMessageBus<LocalMqttMessagingClientOptions> _mqttMessageBus;

        public BackgroundLocalMqttPublisher(
            IMqttTopicClient<LocalMqttMessagingClientOptions> topicClient, 
            IMqttMessageBus<LocalMqttMessagingClientOptions> mqttMessageBus)
        {
            _topicClient = topicClient;
            _mqttMessageBus = mqttMessageBus;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await RegisterMessageHandler<HandlerForDeviceNumber1>(deviceNumber: 1);
            
            await RegisterMessageHandler<HandlerForDeviceNumber2>(deviceNumber: 2);
            
            // await RegisterMessageHandlerForAllDevices<HandlerForAllDeviceNumbers>();
            
            await base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(3), stoppingToken);
            
            while (!stoppingToken.IsCancellationRequested)
            {
                await PublishToDevice(deviceNumber: 1);
                
                // await PublishToDevice(deviceNumber: 2);

                await Task.Delay(TimeSpan.FromSeconds(3), stoppingToken);
            }
        }

        private async Task PublishToDevice(int deviceNumber)
        {
            var deviceTopic = $"device/{deviceNumber}";
            var payload = new DeviceMessagePayload { Name = $"device {deviceNumber}" };
            await _mqttMessageBus.Publish(payload, deviceTopic);
        }

        private async Task RegisterMessageHandler<T>(int deviceNumber) where T: IMessageHandler
        {
            var deviceTopic = $"device/{deviceNumber}";
            await _topicClient.Subscribe<T>(deviceTopic);
        }
        
        private async Task RegisterMessageHandlerForAllDevices<T>() where T: IMessageHandler
        {
            const string deviceTopic = "device/#";
            await _topicClient.Subscribe<T>(deviceTopic);
        }
        
    }
}