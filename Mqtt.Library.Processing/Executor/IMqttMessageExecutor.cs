using MQTTnet;

namespace Mqtt.Library.Processing.Executor;

public interface IMqttMessageExecutor
{
    Task ExecuteAsync(MqttApplicationMessageReceivedEventArgs messageReceivedEventArgs);
}