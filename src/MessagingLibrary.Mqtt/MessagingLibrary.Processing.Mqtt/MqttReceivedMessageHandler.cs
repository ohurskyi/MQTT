using MessagingLibrary.Client.Mqtt.Configuration;
using MessagingLibrary.Processing.Executor;
using MessagingLibrary.Processing.Mqtt.Extensions;
using MQTTnet.Client;

namespace MessagingLibrary.Processing.Mqtt;

public class MqttReceivedMessageHandler<TMessagingClientOptions>
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