using Mqtt.Library.Client.Configuration;
using Mqtt.Library.TopicClient;

namespace Mqtt.Library.Processing.Listeners;

public interface IMqttStartupListener<TMessagingClientOptions> where TMessagingClientOptions : IMqttMessagingClientOptions
{
    IEnumerable<Task<ISubscription>> DefineSubscriptions();
}