using MessagingLibrary.Client.Mqtt;
using MessagingLibrary.Client.Mqtt.Configuration;
using MessagingLibrary.Core.Clients;
using MessagingLibrary.Core.Definitions.Subscriptions;
using MessagingLibrary.Core.Factory;
using MessagingLibrary.Core.Handlers;

namespace MessagingLibrary.TopicClient.Mqtt;

public class MqttTopicClient<TMessagingClientOptions> : ITopicClient<TMessagingClientOptions> where TMessagingClientOptions : class, IMqttMessagingClientOptions
{
    private readonly IMqttMessagingClient<TMessagingClientOptions> _mqttMessagingClient;
    private readonly IMessageHandlerFactory<TMessagingClientOptions> _messageHandlerFactory;

    public MqttTopicClient(IMqttMessagingClient<TMessagingClientOptions> mqttMessagingClient, IMessageHandlerFactory<TMessagingClientOptions> messageHandlerFactory)
    {
        _mqttMessagingClient = mqttMessagingClient;
        _messageHandlerFactory = messageHandlerFactory;
    }

    public async Task<ISubscriptionDefinition> Subscribe<T>(string topic) where T : class, IMessageHandler
    {
        await SubscribeInner<T>(topic);
        return new SubscriptionDefinition<T>(topic);
    }

    public async Task Subscribe(ISubscriptionDefinition subscriptionDefinition)
    {
        await SubscribeInner(subscriptionDefinition);
    }

    public async Task Unsubscribe(ISubscriptionDefinition subscriptionDefinition)
    {
        await UnsubscribeInner(subscriptionDefinition);
    }

    private async Task UnsubscribeInner(ISubscriptionDefinition subscriptionDefinition)
    {
        if (_messageHandlerFactory.RemoveHandler(subscriptionDefinition.HandlerType, subscriptionDefinition.Topic) == 0)
        {
            await _mqttMessagingClient.UnsubscribeAsync(subscriptionDefinition.Topic);
        }
    }

    private async Task SubscribeInner<T>(string topic) where T : class, IMessageHandler
    {
        if (_messageHandlerFactory.RegisterHandler<T>(topic) == 1)
        {
            await _mqttMessagingClient.SubscribeAsync(topic);
        }
    }
    
    private async Task SubscribeInner(ISubscriptionDefinition subscriptionDefinition)
    {
        if (_messageHandlerFactory.RegisterHandler(subscriptionDefinition.HandlerType, subscriptionDefinition.Topic) == 1)
        {
            await _mqttMessagingClient.SubscribeAsync(subscriptionDefinition.Topic);
        }
    }
}