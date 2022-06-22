using MessagingLibrary.Core.Messages;

namespace Mqtt.Library.Unit.Payloads;

public class DeviceMessagePayload : IMessagePayload
{
    public string Name { get; set; }
}