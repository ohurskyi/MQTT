using Mqtt.Library.Core;
using Mqtt.Library.Core.GenericTest;
using Mqtt.Library.MessageBus;
using Mqtt.Library.MessageBus.GenericTest;
using Mqtt.Library.Test.ClientOptions;
using Mqtt.Library.Test.GenericTest;
using Mqtt.Library.Test.Handlers;
using Mqtt.Library.TopicClient;
using Mqtt.Library.TopicClient.GenericTest;
using MQTTnet;

namespace Mqtt.Library.Test
{
    public class BackgroundLocalMqttPublisher : BackgroundService
    {
        private readonly MqttTopicClientGen<LocalMqttMessagingClientOptions> _topicClient;
        private readonly MqttMessageBusGen<LocalMqttMessagingClientOptions> _mqttMessageBus;

        public BackgroundLocalMqttPublisher(MqttTopicClientGen<LocalMqttMessagingClientOptions> topicClient, MqttMessageBusGen<LocalMqttMessagingClientOptions> mqttMessageBus)
        {
            _topicClient = topicClient;
            _mqttMessageBus = mqttMessageBus;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await RegisterMessageHandler<HandlerForDeviceNumber1>(deviceNumber: 1);
            
            //await RegisterMessageHandler<HandlerForDeviceNumber2>(deviceNumber: 2);
            
            // await RegisterMessageHandlerForAllDevices<HandlerForAllDeviceNumbers>();
            
            await base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(3), stoppingToken);
            
            while (!stoppingToken.IsCancellationRequested)
            {
                await PublishToDevice(deviceNumber: 1);
                
                //await PublishToDevice(deviceNumber: 2);

                await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
            }
        }

        private async Task PublishToDevice(int deviceNumber)
        {
            var deviceTopic = $"device/{deviceNumber}";
            var payload = new TestMessagePayload { Name = $"device {deviceNumber}" };
            var message = new Message { Topic = deviceTopic, Payload = payload.ToJson() };
            await _mqttMessageBus.Publish(message);
        }

        private async Task RegisterMessageHandler<T>(int deviceNumber) where T: IMessageHandlerGen
        {
            var deviceTopic = $"device/{deviceNumber}";
            await _topicClient.Subscribe<T>(deviceTopic);
        }
        
        private async Task RegisterMessageHandlerForAllDevices<T>() where T: IMessageHandlerGen
        {
            const string deviceTopic = "device/#";
            await _topicClient.Subscribe<T>(deviceTopic);
        }
        
    }
}