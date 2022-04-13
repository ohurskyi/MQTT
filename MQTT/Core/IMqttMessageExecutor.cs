using MQTTnet;

namespace Mqtt.Library.Test.Core
{
    public interface IMqttMessageExecutor
    {
        Task ExecuteAsync(MqttApplicationMessageReceivedEventArgs messageReceivedEventArgs);
    }
}