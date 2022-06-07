using MessagingLibrary.Core.Configuration;
using MessagingLibrary.Core.Messages;

namespace MessagingLibrary.Processing.Strategy;

public interface IMessageHandlingStrategy<TMessagingClientOptions> where TMessagingClientOptions: IMessagingClientOptions
{
    Task Handle(IMessage message);
}