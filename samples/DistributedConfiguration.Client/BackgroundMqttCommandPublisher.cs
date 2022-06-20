using MessagingLibrary.Core.Messages;
using Mqtt.Library.Client.Local;
using MqttLibrary.Examples.Pairing.Contracts.Payloads;
using MqttLibrary.Examples.Pairing.Contracts.Topics;

namespace DistributedConfiguration.Client
{
    public class BackgroundMqttCommandPublisher : BackgroundService
    {
        private int _msgSendCount = 0;
        
        private readonly IMessageBus<LocalMqttMessagingClientOptions> _mqttMessageBusLocal;

        public BackgroundMqttCommandPublisher(IMessageBus<LocalMqttMessagingClientOptions> mqttMessageBusLocal)
        {
            _mqttMessageBusLocal = mqttMessageBusLocal;
        }


        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var message = new PairDevicePayload { MacAddress = $"Address: {++_msgSendCount}" };
                const string topic = TopicConstants.PairDevice;
                
                await _mqttMessageBusLocal.Publish(message, topic);

                await Task.Delay(TimeSpan.FromSeconds(1), stoppingToken);
            }
        }
    }
}