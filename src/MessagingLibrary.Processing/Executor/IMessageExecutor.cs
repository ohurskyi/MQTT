using Mqtt.Library.Core.Configuration;
using Mqtt.Library.Core.Messages;

namespace MessagingLibrary.Processing.Executor;

public interface IMessageExecutor<TMessagingClientOptions> where TMessagingClientOptions: IMessagingClientOptions
{
    Task ExecuteAsync(IMessage message);
}