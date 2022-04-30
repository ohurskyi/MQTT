using Mqtt.Library.Core.GenericTest;
using Mqtt.Library.Processing.Executor;
using MQTTnet;
using MQTTnet.Client.Receiving;

namespace Mqtt.Library.Processing;

public class MqttReceivedMessageHandlerGen : IMqttApplicationMessageReceivedHandler
{
    private readonly IMqttMessageExecutor _messageExecutor;

    public MqttReceivedMessageHandlerGen(IMqttMessageExecutor messageExecutor)
    {
        _messageExecutor = messageExecutor;
    }
    
    public async Task HandleApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs eventArgs)
    {
        await _messageExecutor.ExecuteAsync(eventArgs);
    }
}