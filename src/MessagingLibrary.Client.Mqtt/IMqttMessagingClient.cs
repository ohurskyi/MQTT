using MQTTnet;
using MQTTnet.Client.Publishing;
using MQTTnet.Client.Receiving;

namespace MessagingLibrary.Client.Mqtt;

public interface IMqttMessagingClient
{
    Task StartAsync();

    Task StopAsync();

    Task SubscribeAsync(string topic);

    Task SubscribeAsync(IEnumerable<string> topic);
        
    Task UnsubscribeAsync(string topic);
        
    Task UnsubscribeAsync(IEnumerable<string> topics);
        
    Task<MqttClientPublishResult> PublishAsync(MqttApplicationMessage mqttApplicationMessage);

    void UseMqttMessageReceivedHandler(IMqttApplicationMessageReceivedHandler handler);
}