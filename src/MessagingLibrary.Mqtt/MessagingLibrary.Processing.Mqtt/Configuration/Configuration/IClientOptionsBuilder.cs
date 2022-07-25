using MQTTnet.Extensions.ManagedClient;

namespace MessagingLibrary.Processing.Mqtt.Configuration.Configuration;

public interface IClientOptionsBuilder<in TMessagingClientOptions> where TMessagingClientOptions : IMqttMessagingClientOptions
{
    ManagedMqttClientOptions BuildClientOptions();
}