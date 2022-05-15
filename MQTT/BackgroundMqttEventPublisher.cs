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
            await base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(3), stoppingToken);
            
            while (!stoppingToken.IsCancellationRequested)
            {
                await PublishToDevice(deviceNumber: 1);

                // await PublishToDevice(deviceNumber: 2);

                await Task.Delay(TimeSpan.FromSeconds(2), stoppingToken);
            }
        }

        private async Task PublishToDevice(int deviceNumber)
        {
            var deviceTopic = $"device/{deviceNumber}";
            var payload = new DeviceMessagePayload { Name = $"device {deviceNumber}" };
            await _mqttMessageBus.Publish(payload, deviceTopic);
        }
    }
}