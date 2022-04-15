using Mqtt.Library.MessageBus;
using Mqtt.Library.Test.Handlers;
using Mqtt.Library.Test.Local;
using Mqtt.Library.TopicClient;
using MQTTnet;

namespace Mqtt.Library.Test
{
    public class BackgroundMqttPublisher : BackgroundService
    {
        private readonly IMqttTopicClient<LocalMqttMessagingClientOptions> _topicClient;
        private readonly IMqttMessageBus<LocalMqttMessagingClientOptions> _mqttMessageBus;

        public BackgroundMqttPublisher(IMqttTopicClient<LocalMqttMessagingClientOptions> topicClient, IMqttMessageBus<LocalMqttMessagingClientOptions> mqttMessageBus)
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