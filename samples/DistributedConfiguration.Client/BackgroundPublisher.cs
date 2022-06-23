using DistributedConfiguration.Contracts.Payloads;
using DistributedConfiguration.Contracts.Topics;
using MessagingLibrary.Core.Messages;
using Mqtt.Library.Client.Local;

namespace DistributedConfiguration.Client
{
    public class BackgroundPublisher : BackgroundService
    {
        private int _msgSendCount = 0;
        
        private readonly IMessageBus<LocalMqttMessagingClientOptions> _mqttMessageBusLocal;

        public BackgroundPublisher(IMessageBus<LocalMqttMessagingClientOptions> mqttMessageBusLocal)
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