using MessagingLibrary.Client.Mqtt;
using MessagingLibrary.Client.Mqtt.Configuration;
using MessagingLibrary.Core.Factory;
using MessagingLibrary.Core.Handlers;
using MessagingLibrary.TopicClient.Mqtt.Definitions.Subscriptions;

namespace MessagingLibrary.TopicClient.Mqtt;

public class MqttTopicClient<TMessagingClientOptions> : IMqttTopicClient<TMessagingClientOptions> where TMessagingClientOptions : class, IMqttMessagingClientOptions
{
    private readonly IMqttMessagingClient<TMessagingClientOptions> _mqttMessagingClient;
    private readonly IMessageHandlerFactory<TMessagingClientOptions> _messageHandlerFactory;

    public MqttTopicClient(IMqttMessagingClient<TMessagingClientOptions> mqttMessagingClient, IMessageHandlerFactory<TMessagingClientOptions> messageHandlerFactory)
    {
        _mqttMessagingClient = mqttMessagingClient;
        _messageHandlerFactory = messageHandlerFactory;
    }

    public async Task<ISubscription> Subscribe<T>(string topic) where T : class, IMessageHandler
    {
        await SubscribeInner<T>(topic);
        return new Subscription<T>(topic);
    }

    public async Task Subscribe(ISubscription subscription)
    {
        await SubscribeInner(subscription);
    }

    public async Task Unsubscribe(ISubscription subscription)
    {
        await UnsubscribeInner(subscription);
    }

    private async Task UnsubscribeInner(ISubscription subscription)
    {
        if (_messageHandlerFactory.RemoveHandler(subscription.HandlerType, subscription.Topic) == 0)
        {
            await _mqttMessagingClient.UnsubscribeAsync(subscription.Topic);
        }
    }

    private async Task SubscribeInner<T>(string topic) where T : class, IMessageHandler
    {
        if (_messageHandlerFactory.RegisterHandler<T>(topic) == 1)
        {
            await _mqttMessagingClient.SubscribeAsync(topic);
        }
    }
    
    private async Task SubscribeInner(ISubscription subscription)
    {
        if (_messageHandlerFactory.RegisterHandler(subscription.HandlerType, subscription.Topic) == 1)
        {
            await _mqttMessagingClient.SubscribeAsync(subscription.Topic);
        }
    }
}