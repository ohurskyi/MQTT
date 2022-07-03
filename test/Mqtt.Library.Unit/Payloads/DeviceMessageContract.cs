using MessagingLibrary.Core.Messages;

namespace Mqtt.Library.Unit.Payloads;

public class DeviceMessageContract : IMessageContract
{
    public string Name { get; set; }
}