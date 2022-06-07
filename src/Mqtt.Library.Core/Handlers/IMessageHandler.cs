using Mqtt.Library.Core.Messages;
using Mqtt.Library.Core.Results;

namespace Mqtt.Library.Core.Handlers;

public interface IMessageHandler
{
    Task<IExecutionResult> Handle(IMessage message);
}