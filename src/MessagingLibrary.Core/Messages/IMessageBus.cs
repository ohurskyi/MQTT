using MessagingLibrary.Core.Configuration;

namespace MessagingLibrary.Core.Messages;

public interface IMessageBus<TMessagingClientOptions> where TMessagingClientOptions : IMessagingClientOptions
{
    Task Publish(IMessageContract contract, string topic);
    Task Publish(IMessage message);
}