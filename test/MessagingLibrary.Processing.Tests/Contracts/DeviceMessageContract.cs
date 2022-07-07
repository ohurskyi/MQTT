using MessagingLibrary.Core.Messages;

namespace MessagingLibrary.Processing.Tests.Contracts;

public class DeviceMessageContract : IMessageContract
{
    public string Name { get; set; }
}