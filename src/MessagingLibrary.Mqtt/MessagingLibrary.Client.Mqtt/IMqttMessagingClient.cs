using MQTTnet;
using MQTTnet.Client;

namespace MessagingLibrary.Client.Mqtt;

public interface IMqttMessagingClient
{
    Task StartAsync();

    Task StopAsync();

    Task SubscribeAsync(string topic);

    Task SubscribeAsync(IEnumerable<string> topics);
        
    Task UnsubscribeAsync(string topic);
        
    Task UnsubscribeAsync(IEnumerable<string> topics);

    Task PublishAsync(MqttApplicationMessage mqttApplicationMessage);

    void UseMqttMessageReceivedHandler(Func<MqttApplicationMessageReceivedEventArgs, Task> handler);
}