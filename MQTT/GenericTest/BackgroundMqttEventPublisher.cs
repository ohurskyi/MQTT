using Mqtt.Library.Core;
using Mqtt.Library.Core.GenericTest;
using Mqtt.Library.MessageBus;
using Mqtt.Library.MessageBus.GenericTest;
using Mqtt.Library.Test.ClientOptions;
using Mqtt.Library.Test.Handlers;
using Mqtt.Library.TopicClient;
using Mqtt.Library.TopicClient.GenericTest;
using MQTTnet;
using Newtonsoft.Json;

namespace Mqtt.Library.Test.GenericTest
{
    public class BackgroundGenMqttPublisher : BackgroundService
    {
        private readonly MqttTopicClientGen<LocalMqttMessagingClientOptions> _topicClient;
        private readonly MqttMessageBusGen<LocalMqttMessagingClientOptions> _mqttMessageBus;

        public BackgroundGenMqttPublisher(MqttTopicClientGen<LocalMqttMessagingClientOptions> topicClient, MqttMessageBusGen<LocalMqttMessagingClientOptions> mqttMessageBus)
        {
            _topicClient = topicClient;
            _mqttMessageBus = mqttMessageBus;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            await RegisterMessageHandler<MessageHandlerGenTest>(deviceNumber: 1);
            
            await base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(3), stoppingToken);
            
            while (!stoppingToken.IsCancellationRequested)
            {
                await PublishToDevice(deviceNumber: 1);
                
                await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
            }
        }

        private async Task PublishToDevice(int deviceNumber)
        { 
            var deviceTopic = $"device/{deviceNumber}";
            var payload = new TestMessagePayload { Name = "device test" };
            var message = new Message { Topic = deviceTopic, Payload = payload.ToJson() };
            await _mqttMessageBus.Publish(message);
        }

        private async Task RegisterMessageHandler<T>(int deviceNumber) where T: IMessageHandlerGen
        {
            var deviceTopic = $"device/{deviceNumber}";
            await _topicClient.Subscribe<T>(deviceTopic);
        }
    }
}