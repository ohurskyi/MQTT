using MessagingLibrary.Core.Factory;

namespace MessagingLibrary.Processing.Mqtt;

public class MqttTopicComparer : ITopicFilterComparer
{
    public bool IsMatch(string topic, string filter)
    {
        return MQTTnet.Server.MqttTopicFilterComparer.IsMatch(topic, filter);
    }
}