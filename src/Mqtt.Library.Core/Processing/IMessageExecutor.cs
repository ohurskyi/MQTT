using Mqtt.Library.Core.Configuration;
using Mqtt.Library.Core.Messages;

namespace Mqtt.Library.Core.Processing;

public interface IMessageExecutor<TMessagingClientOptions> where TMessagingClientOptions: IMessagingClientOptions
{
    Task ExecuteAsync(IMessage message);
}