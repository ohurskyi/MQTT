using MessagingLibrary.Processing.Executor;
using MessagingLibrary.Processing.Mqtt.Extensions;
using Mqtt.Library.Client.Configuration;
using MQTTnet;
using MQTTnet.Client.Receiving;

namespace MessagingLibrary.Processing.Mqtt;

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