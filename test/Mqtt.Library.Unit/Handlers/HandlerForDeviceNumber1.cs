using System.IO;
using System.Threading.Tasks;
using MessagingLibrary.Core.Handlers;
using MessagingLibrary.Core.Messages;
using MessagingLibrary.Core.Results;
using Mqtt.Library.Unit.Payloads;

namespace Mqtt.Library.Unit.Handlers;

public class HandlerForDeviceNumber1 : MessageHandlerBase<DeviceMessagePayload>
{
    private readonly TextWriter _textWriter;

    public HandlerForDeviceNumber1(TextWriter textWriter)
    {
        _textWriter = textWriter;
    }
    
    protected override async Task<IExecutionResult> HandleAsync(MessagingContext<DeviceMessagePayload> messagingContext)
    {
        var payload = messagingContext.Payload;
        await _textWriter.WriteLineAsync(payload.Name + " " + nameof(HandlerForDeviceNumber1));
        return ExecutionResult.Ok();
    }
}