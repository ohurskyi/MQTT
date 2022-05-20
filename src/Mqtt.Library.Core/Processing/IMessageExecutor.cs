using Mqtt.Library.Core.Messages;

namespace Mqtt.Library.Core.Processing;

public interface IMessageExecutor<T> where T: class
{
    Task ExecuteAsync(IMessage message);
}