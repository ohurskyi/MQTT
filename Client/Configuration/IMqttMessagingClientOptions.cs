namespace Mqtt.Library.Test.Client.Configuration
{
    public interface IMqttMessagingClientOptions
    {
        MqttBrokerConnectionOptions MqttBrokerConnectionOptions { get; set; }
    }
}