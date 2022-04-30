using Mqtt.Library.Core;
using Mqtt.Library.Core.Messages;
using Mqtt.Library.MessageBus;
using Mqtt.Library.Test.ClientOptions;
using Mqtt.Library.Test.Handlers;
using Mqtt.Library.Test.Payloads;
using Mqtt.Library.TopicClient;

namespace Mqtt.Library.Test;

public class BackgroundTestMqttPublisher : BackgroundService
{
    private readonly IMqttTopicClient<TestMqttMessagingClientOptions> _topicClient;
    private readonly IMqttMessageBus<TestMqttMessagingClientOptions> _mqttMessageBus;

    public BackgroundTestMqttPublisher(IMqttTopicClient<TestMqttMessagingClientOptions> topicClient, IMqttMessageBus<TestMqttMessagingClientOptions> mqttMessageBus)
    {
        _topicClient = topicClient;
        _mqttMessageBus = mqttMessageBus;
    }

    public override async Task StartAsync(CancellationToken cancellationToken)
    {
        await RegisterMessageHandler<HandlerForDeviceNumber1ForAnotherClient>(deviceNumber: 1);

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
        var payload = new DeviceMessagePayload { Name = $"device {deviceNumber}" };
        var message = new Message { Topic = deviceTopic, Payload = payload.ToJson() };
        await _mqttMessageBus.Publish(message);
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