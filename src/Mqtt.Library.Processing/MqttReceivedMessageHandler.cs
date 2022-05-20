using Mqtt.Library.Client.Configuration;
using Mqtt.Library.Core.Processing;
using Mqtt.Library.Processing.Extensions;
using MQTTnet;
using MQTTnet.Client.Receiving;

namespace Mqtt.Library.Processing;

public class MqttReceivedMessageHandler<TMessagingClientOptions> : IMqttApplicationMessageReceivedHandler
    where TMessagingClientOptions : class, IMqttMessagingClientOptions
{
    private readonly IMessageExecutor<TMessagingClientOptions> _messageExecutor;

    public MqttReceivedMessageHandler(IMessageExecutor<TMessagingClientOptions> messageExecutor)
    {
        _messageExecutor = messageExecutor;
    }

    public async Task HandleApplicationMessageReceivedAsync(MqttApplicationMessageReceivedEventArgs eventArgs)
    {
        await _messageExecutor.ExecuteAsync(eventArgs.ApplicationMessage.ToMessage());
    }
}