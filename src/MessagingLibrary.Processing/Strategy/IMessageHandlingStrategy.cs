using Mqtt.Library.Core.Configuration;
using Mqtt.Library.Core.Messages;

namespace MessagingLibrary.Processing.Strategy;

public interface IMessageHandlingStrategy<TMessagingClientOptions> where TMessagingClientOptions: IMessagingClientOptions
{
    Task Handle(IMessage message);
}