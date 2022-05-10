using Mqtt.Library.Core.Messages;

namespace Mqtt.Library.Core.Processing;

public interface IMessageExecutor
{
    Task ExecuteAsync(IMessage message);
}