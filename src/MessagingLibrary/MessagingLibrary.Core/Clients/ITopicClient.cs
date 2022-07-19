using MessagingLibrary.Core.Configuration;
using MessagingLibrary.Core.Definitions.Subscriptions;
using MessagingLibrary.Core.Handlers;

namespace MessagingLibrary.Core.Clients;

public interface ITopicClient<TMessagingClientOptions> where TMessagingClientOptions : IMessagingClientOptions
{
    Task<ISubscriptionDefinition> Subscribe<T>(string topic) where T : class, IMessageHandler;
    Task Subscribe(ISubscriptionDefinition definition);
    Task Unsubscribe(ISubscriptionDefinition subscriptionDefinition);
}