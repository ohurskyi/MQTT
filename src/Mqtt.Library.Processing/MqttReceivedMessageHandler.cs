using Mqtt.Library.Client.Configuration;
using Mqtt.Library.Core.Processing;
using Mqtt.Library.Processing.Extensions;
using MQTTnet;
using MQTTnet.Client.Receiving;

namespace Mqtt.Library.Processing;

public class MqttReceivedMessageHandler<TMessagingClientOptions> : IMqttApplicationMessageReceivedHandler
    where TMessagingClientOptions : IMqttMessagingClientOptions
{
    private readonly IMessageExecutor _messageExecutor;

    public MqttReceivedMessageHandler(IMessageExecutor messageExecutor)
    {
        _messageExecutor = messageExecutor;
    }

    public async Task HandleApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs eventArgs)
    {
        await _messageExecutor.ExecuteAsync(eventArgs.ApplicationMessage.ToMessage());
    }
}