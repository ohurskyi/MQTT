using System.IO;
using System.Threading.Tasks;
using MessagingLibrary.Core.Handlers;
using MessagingLibrary.Core.Messages;
using MessagingLibrary.Core.Results;
using MessagingLibrary.Processing.Tests.Contracts;

namespace MessagingLibrary.Processing.Tests.Handlers;

public class HandlerForAllDeviceNumbers : MessageHandlerBase<DeviceMessageContract>
{
    private readonly TextWriter _textWriter;

    public HandlerForAllDeviceNumbers(TextWriter textWriter)
    {
        _textWriter = textWriter;
    }

    protected override async Task<IExecutionResult> HandleAsync(MessagingContext<DeviceMessageContract> messagingContext)
    {
        var payload = messagingContext.Payload;
        await _textWriter.WriteLineAsync(payload.Name + " " + nameof(HandlerForAllDeviceNumbers));
        return ExecutionResult.Ok();
    }
}