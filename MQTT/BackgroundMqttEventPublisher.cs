using Mqtt.Library.MessageBus;
using Mqtt.Library.Test.ClientOptions;
using Mqtt.Library.Test.Handlers;
using Mqtt.Library.Test.Payloads;

namespace Mqtt.Library.Test
{
    public class BackgroundLocalMqttPublisher : BackgroundService
    {
        private readonly IMqttMessageBus<LocalMqttMessagingClientOptions> _mqttMessageBusLocal;
        private readonly IMqttMessageBus<TestMqttMessagingClientOptions> _mqttMessageBusTest;

        public BackgroundLocalMqttPublisher(IMqttMessageBus<LocalMqttMessagingClientOptions> mqttMessageBus, IMqttMessageBus<TestMqttMessagingClientOptions> mqttMessageBusTest)
        {
            _mqttMessageBusLocal = mqttMessageBus;
            _mqttMessageBusTest = mqttMessageBusTest;
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
                await Task.WhenAll(
                    PublishToDeviceUsingLocalClient(deviceNumber: 1),
                    PublishToDeviceUsingTestClient(deviceNumber: 1)
                    );

                // await PublishToDevice(deviceNumber: 2);

                await Task.Delay(TimeSpan.FromSeconds(5), stoppingToken);
            }
        }

        private async Task PublishToDeviceUsingLocalClient(int deviceNumber)
        {
            var deviceTopic = $"device/{deviceNumber}";
            var payload = new DeviceMessagePayload { Name = $"device {deviceNumber}" };
            await _mqttMessageBusLocal.Publish(payload, deviceTopic);
        }
        
        private async Task PublishToDeviceUsingTestClient(int deviceNumber)
        {
            var deviceTopic = $"device/{deviceNumber}";
            var payload = new DeviceMessagePayload { Name = $"device {deviceNumber}" };
            await _mqttMessageBusTest.Publish(payload, deviceTopic);
        }
    }
}