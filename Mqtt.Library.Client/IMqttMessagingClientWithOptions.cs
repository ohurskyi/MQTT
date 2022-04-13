using Mqtt.Library.Client.Configuration;

namespace Mqtt.Library.Client;

public interface IMqttMessagingClient<in TMessagingClientOptions> : IMqttMessagingClient
    where TMessagingClientOptions : IMqttMessagingClientOptions
{
}