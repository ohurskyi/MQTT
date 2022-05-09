using Mqtt.Library.Core.Messages;

namespace Mqtt.Library.Processing.Executor;

public interface IMessageExecutor
{
    Task ExecuteAsync(IMessage message);
}