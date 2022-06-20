using MessagingLibrary.Core.Configuration;

namespace MessagingLibrary.Core.Messages;

public interface IMessageBus<TMessagingClientOptions> where TMessagingClientOptions : IMessagingClientOptions
{
    Task Publish(IMessagePayload payload, string topic);
    Task Publish(IMessage message);
}