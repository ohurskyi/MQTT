using Mqtt.Library.Test.Core;
using Mqtt.Library.Test.Handlers;
using MqttClientTest.Messaging.Processing;
using MQTTnet;

namespace Mqtt.Library.Test
{
    public class BackgroundMqttPublisher : BackgroundService
    {
        private readonly IMessageHandlerFactory _messageHandlerFactory;
        private readonly IMqttMessageExecutor _mqttMessageExecutor;

        public BackgroundMqttPublisher(IMessageHandlerFactory messageHandlerFactory, IMqttMessageExecutor mqttMessageExecutor)
        {
            _messageHandlerFactory = messageHandlerFactory;
            _mqttMessageExecutor = mqttMessageExecutor;
        }

        public override async Task StartAsync(CancellationToken cancellationToken)
        {
            var topic = "some-topic";
            _messageHandlerFactory.RegisterHandler<MessageHandlerTest>(topic);
            _messageHandlerFactory.RegisterHandler<MessageHandlerTest2>(topic);
            
            await base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.Delay(TimeSpan.FromSeconds(3), stoppingToken);
            
            while (!stoppingToken.IsCancellationRequested)
            {
                var topic = "some-topic";
                var mqttMessage = new MqttApplicationMessageBuilder().WithTopic(topic);
                await _mqttMessageExecutor.ExecuteAsync(new MqttApplicationMessageReceivedEventArgs(string.Empty, mqttMessage.Build()));
                await Task.Delay(TimeSpan.FromSeconds(10), stoppingToken);
            }
        }
    }
}