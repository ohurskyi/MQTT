﻿using DistributedConfiguration.Contracts.Pairing;
using DistributedConfiguration.Contracts.Topics;
using MessagingLibrary.Core.Clients;
using MessagingLibrary.Core.Messages;
using Mqtt.Library.Client.Infrastructure;

namespace DistributedConfiguration.Client
{
    public class BackgroundPublisher : BackgroundService
    {
        private int _msgSendCount = 0;
        
        private readonly IMessageBus<InfrastructureMqttMessagingClientOptions> _mqttMessageBusLocal;

        public BackgroundPublisher(IMessageBus<InfrastructureMqttMessagingClientOptions> mqttMessageBusLocal)
        {
            _mqttMessageBusLocal = mqttMessageBusLocal;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var message = new PairDeviceContract { MacAddress = $"Address: {++_msgSendCount}" };
                
                const string topic = DistributedConfigurationTopicConstants.PairDevice;
                
                await _mqttMessageBusLocal.Publish(message, topic);

                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
            }
        }
    }
}