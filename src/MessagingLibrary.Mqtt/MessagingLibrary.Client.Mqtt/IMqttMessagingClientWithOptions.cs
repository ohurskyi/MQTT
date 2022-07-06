using MessagingLibrary.Client.Mqtt.Configuration;

namespace MessagingLibrary.Client.Mqtt;

public interface IMqttMessagingClient<in TMessagingClientOptions> : IMqttMessagingClient
    where TMessagingClientOptions : IMqttMessagingClientOptions
{
}