using Mqtt.Library.Client.Configuration;

namespace Mqtt.Library.Processing.Listeners;

public interface IMqttStartupListener<TMessagingClientOptions> where TMessagingClientOptions : IMqttMessagingClientOptions
{
    Task CreateSubscriptions();
}