using Mqtt.Library.Core.Processing;
using Mqtt.Library.Processing.Extensions;
using MQTTnet;
using MQTTnet.Client.Receiving;

namespace Mqtt.Library.Processing;

public class MqttReceivedMessageHandlerGen : IMqttApplicationMessageReceivedHandler
{
    private readonly IMessageExecutor _messageExecutor;

    public MqttReceivedMessageHandlerGen(IMessageExecutor messageExecutor)
    {
        _messageExecutor = messageExecutor;
    }
    
    public async Task HandleApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs eventArgs)
    {
        await _messageExecutor.ExecuteAsync(eventArgs.ApplicationMessage.ToMessage());
    }
}